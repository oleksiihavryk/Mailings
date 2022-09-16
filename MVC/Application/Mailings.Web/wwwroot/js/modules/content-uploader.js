class ContentUploader
{
    constructor(file) {
        //fields
        this['#file'] = file;
    }

    uploadContent(callback = undefined) {
        const file = this['#file'];

        if (file.type !== 'text/plain' && file.type !== 'text/html') {
            return false;
        }

        const reader = new FileReader();

        reader.onload = function () {
            const res = this.result;

            if (callback !== undefined) {
                callback(res);
            }
        }

        reader.readAsText(file, 'utf-15');

        return true;
    }
}