$(function () {
    const isUnavailable = 'Current feature is unavailable';

    $('#Facebook').click(handleUnavailableFeature);
    $('#Gmail').click(handleUnavailableFeature);

    function handleUnavailableFeature() {
        const modal = new Modal(isUnavailable);
        modal.invokeModalWindow();
    }
});