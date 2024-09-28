// ajaxUtility.js
export function get(url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        async: false,
        success: function (response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function (error) {
            if (errorCallback) {
                errorCallback(error);
            }
        }
    });
}

export function post(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function (error) {
            if (errorCallback) {
                errorCallback(error);
            }
        }
    });
}
