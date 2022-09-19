class Modal {
    constructor(text) {
        //private fields
        this["#animTime"] = 125;
        this["#text"] = text;

        //private method
        this["#stopClickPropagation"] = function(e) {
            e.stopPropagation();
        }
        this["#animObjectOut"] = {
            marginTop: '-=20%'
        };
        this["#animObjectIn"] = {
            marginTop: '+=20%'
        };
    }

    get text() {
        return this["#text"];
    }
    set text(text) {
        this["#text"] = text;
    }

    invokeModalWindow() {
        const struct = $(`<div id="modal" class="modal-window">` +
            `<div class="modal-block">` +
            `<span id="close" class="button modal-close-button">close</span>` +
            `<p class="modal-block-text">${this['#text']}</p>` +
            `</div>` +
            `</div>`);

        //anim
        struct.prependTo('body').fadeIn(this['#animTime']);
        const animObject = this["#animObjectIn"];
        struct.children('.modal-block').animate(
            animObject,
            this['#animTime']);;

        const modalWindow = $('#modal');

        modalWindow.click(this.closeModalWindow.bind(this));
        modalWindow.children('.modal-block').click(this['#stopClickPropagation']);

        $('#close').click(this.closeModalWindow.bind(this));
    }
    closeModalWindow(e) {
        e.stopPropagation();
        const modal = $('#modal');
        //anim and remove
        const animObject = this["#animObjectOut"];
        const anim = this["#animTime"];

        modal.fadeOut(anim);
        modal.children('.modal-block').animate(
            animObject,
            anim,
            function () {
                modal.remove();
            });
    }
}   
$(function () {
    $('#copy').click(copyAccountDataToClipboard);
    $('.data-for-copying').click(copyDataUnitToClipboard);

    function copyDataUnitToClipboard() {
        //copying in buffer
        const dataForCopying = getUnitDataForCopying(this); // {}
        copyDataToClipboard(dataForCopying);
        //invoke model window with text
        const text = `Data '${$(this).data('defenition')}' is copied in a clipboard!`;
        const modal = new Modal(text);
        modal.invokeModalWindow();
    }
    function copyAccountDataToClipboard() {
        //copying in buffer
        const dataForCopying = getAccountDataForCopying(); // {}
        copyDataToClipboard(dataForCopying);
        //invoke model window with text
        const text = 'Account data is copied in a clipboard!';
        const modal = new Modal(text);
        modal.invokeModalWindow();
    }


    function getUnitDataForCopying(el) {
        const data = {};
        const jqEl = $(el);

        data[jqEl.data('defenition')] = jqEl.text();

        return data;
    }
    function getAccountDataForCopying() {
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
    function copyDataToClipboard(dataForCopying) {
        const stringArray = [];
        for (let el in dataForCopying) {
            stringArray.push(el + ': ' + dataForCopying[el]);
        }
        const text = stringArray.join(', ');

        copyToClipboard(text);
    }
    function copyToClipboard(text) {
        {
            const el = document.createElement('textarea');
            el.value = text;
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
});