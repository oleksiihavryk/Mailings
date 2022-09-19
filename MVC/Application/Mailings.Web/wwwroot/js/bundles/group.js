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
$(() => {
    const animTime = 150;

    $("#AddReceiverButton").click(addReceiverFieldToEnd);
    $("#RemoveReceiverButton").click(removeLastReceiverField);

    function addReceiverFieldToEnd() {
        const rs = $("#ReceiversSection");
        if (rs.length !== 0) {
            addReceiverTo(rs);
        } else {
            const newRs = createReceiverSection();
            addReceiverTo(newRs);
        }
    }
    function removeLastReceiverField() {
        const rs = $("#ReceiversSection");
        if (rs.children().length === 1) {
            rs.slideUp(150, function() {
                    this.remove();
                });
        } else if (rs.length !== 0) {
            rs.children().last().slideUp(150,
                function() {
                    this.remove();
                });
        } else {
            const text = "You cant delete a receiver from group, " +
                "because you does not have any receivers";
            const modal = new Modal(text);

            modal.invokeModalWindow();
        }
    }

    function addReceiverTo(receiverSection) {
        const num = receiverSection.children('.form-group').length;
        const el = $(`<div class="form-group">
                         <label for="To_${num}_">Email ${num + 1}</label>
                         <input type="text" id="To_${num}_" name="To[${num}]">
                      </div>`);

        el.hide();
        receiverSection.append(el);
        el.slideDown(animTime);
    }
    function createReceiverSection() {
        const el = $(`<div id="ReceiversSection" class="form-section">
                     </div>`);

        $("div.buttons-section").before(el);

        return el;
    }
});