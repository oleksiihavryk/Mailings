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