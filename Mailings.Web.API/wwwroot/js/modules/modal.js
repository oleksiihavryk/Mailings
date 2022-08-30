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

    async invokeModalWindow() {
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