$(function () {
    const animTime = 150;

    const copyButton = $('#copy');

    copyButton.click(copyToBuffer);

    function copyToBuffer() {
        //copying in buffer
        const dataForCopying = getDataForCopying(); // {}
        copyToUserCopyingBuffer(dataForCopying);
        //invoke model window with text
        const text = 'Account data is copied in a clipboard!';
        invokeModalWindow(text);
    }
    function getDataForCopying() {
        const data = {};
        const elems = $('.data-for-copying');

        elems.each(function (i) {
            const el = $(this);

            const elData = el.html().trim(' ');
            const def = el.data("defenition");

            data[def] = elData;
        });

        return data;
    }
    function copyToUserCopyingBuffer(dataForCopying) {
        const stringArray = [];
        for (let el in dataForCopying) {
            stringArray.push(el + ': ' + dataForCopying[el]);
        }
        const text = stringArray.join(', ');

        copyToClipboard(text);
    }
    function copyToClipboard(str) {
        {
            const el = document.createElement('textarea');
            el.value = str;
            el.setAttribute('readonly', '');
            el.style.position = 'absolute';
            el.style.left = '-9999px';
            document.body.appendChild(el);
            const selected =
                document.getSelection().rangeCount > 0
                    ? document.getSelection().getRangeAt(0)
                    : false;
            el.select();
            document.execCommand('copy');
            document.body.removeChild(el);
            if (selected) {
                document.getSelection().removeAllRanges();
                document.getSelection().addRange(selected);
            }
        }
    }
    function invokeModalWindow(text) {
        const struct = $(`<div id="modal" class="modal-window">` +
                           `<div class="modal-block">` +
                               `<span id="close" class="button modal-close-button">close</span>` +
                               `<p class="modal-block-text">${text}</p>` + 
                            `</div>` +
                         `</div>`);

        //anim
        struct.prependTo('body').fadeIn(animTime);
        struct.children('.modal-block').animate({
            margin: '20% auto'
        }, animTime);;

        const modalWindow = $('#modal');

        modalWindow.click(closeModalWindow);
        modalWindow.children('.modal-block').click(stopClickPropagation);

        $('#close').click(closeModalWindow);
    }
    function closeModalWindow(e) {
        e.stopPropagation();
        const modal = $('#modal');

        //anim and remove
        modal.fadeOut(animTime);
        modal.children('.modal-block').animate({
            margin: '-10% auto'
        }, animTime, function () {
            modal.remove();
        });
    }
    function stopClickPropagation(e) {
        e.stopPropagation();
    }
});