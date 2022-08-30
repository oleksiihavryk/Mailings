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