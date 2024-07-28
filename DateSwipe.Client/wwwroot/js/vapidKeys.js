console.log("vapidKeys.js loaded");

window.validateVapidKeys = function (publicKey, privateKey) {
    console.log("validateVapidKeys function called");

    function isValidBase64Url(base64UrlString) {
        const base64UrlPattern = /^[A-Za-z0-9-_]+$/;
        return base64UrlPattern.test(base64UrlString);
    }

    const publicKeyValid = isValidBase64Url(publicKey);
    const privateKeyValid = isValidBase64Url(privateKey);

    const result = {
        publicKey: publicKey,
        isPublicKeyValid: publicKeyValid,
        privateKey: privateKey,
        isPrivateKeyValid: privateKeyValid
    };

    return result;
};
