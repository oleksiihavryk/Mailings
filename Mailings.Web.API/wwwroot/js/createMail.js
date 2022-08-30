$(function () {
    const addRemoveAnimTime = 150;

    $("#RemoveAttachmentButton").click(handleRemoveAttachment);
    $("#AddAttachmentButton").click(handleAddAttachment);
    $('#Attachments').change(addNewFileToAttachment);
    $('#UploadContent').change(addContentToTextarea);

    function handleAddAttachment() {
        if ($("#Attachments").length !== 1) {
            createAttachment();
        } else {
            const text = "Attachments is already added to the page";
            const modal = new Modal(text);

            modal.invokeModalWindow();
        }
    }
    function handleRemoveAttachment() {
        if ($("div.attachment-group").length === 1) {
            $("div.attachment-group").last().parent().slideUp(addRemoveAnimTime, function () {
                $(this).remove();
            });
        } else {
            const text = 'Attachments is not included to the page';
            const modal = new Modal(text);

            modal.invokeModalWindow();
        }
    }
    function addContentToTextarea() {
        const file = this.files[0];
        var isUpload = new ContentUploader(file).uploadContent(addToTextarea);
        if (isUpload) {
            $("div.upload-group .file-choosen").text('Now using: ' + file.name);
        } else {
            const text = 'Loaded file must be .txt or .html format!';
            const modal = new Modal(text);

            modal.invokeModalWindow();
        }
    }
    function addNewFileToAttachment() {
        const names = [];
        for (let el of this.files) {
            names.push(el.name);
        }
        let text = names.join(', ');
        if (text.length > 15) {
            text = text.substring(0, 15) + '...';
        }
        $(this).siblings('.file-choosen').text(text.length > 0 ? text : '<unknown file>');
    }


    function createAttachment()
    {
        const attachments = $(`<div class="form-section">
                                   <h3 class="form-section-label">Attachments</h3>
                                    <div class="form-group attachment-group">
                                        <label class="upload-button" for="Attachments">
                                            Upload attachments
                                        </label>
                                        <p class="file-choosen">File is not choosen</p>
                                        <input hidden="" multiple="" type="file"
                                            data-val="true" data-val-required="The Attachments field is required."
                                            id="Attachments" name="Attachments">
                                    </div>
                               </div>`);
        attachments.slideUp(function() {
            $(".buttons-section").before(attachments);
        });
        attachments.find('#Attachments').change(addNewFileToAttachment);
        attachments.slideDown(addRemoveAnimTime);
    }
    function addToTextarea(content) {
        $("#Content").val(content);
    }
});