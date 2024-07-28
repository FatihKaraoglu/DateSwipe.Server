window.requestPushNotificationPermission = async function () {
    const permission = await Notification.requestPermission();
    return permission;
};

window.subscribeToPushNotifications = async function () {
    try {
        const registration = await navigator.serviceWorker.ready;
        const subscription = await registration.pushManager.subscribe({
            userVisibleOnly: true,
            applicationServerKey: urlBase64ToUint8Array('BKB8tH8UnOujhiUhYGxUFmaz3Dlz48NLycUFPuBRMcDikT63O-1SHWoWFiluPMA8OzgtPA4lsRkCVuYnAQfCY90')
        });

        return {
            endpoint: subscription.endpoint,
            keys: {
                p256dh: btoa(String.fromCharCode.apply(null, new Uint8Array(subscription.getKey('p256dh')))),
                auth: btoa(String.fromCharCode.apply(null, new Uint8Array(subscription.getKey('auth'))))
            }
        };
    } catch (error) {
        console.error('Error during push subscription:', error);
        throw error;
    }
};

function urlBase64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding).replace(/-/g, '+').replace(/_/g, '/');
    try {
        const rawData = window.atob(base64);
        const outputArray = new Uint8Array(rawData.length);
        for (let i = 0; i < rawData.length; ++i) {
            outputArray[i] = rawData.charCodeAt(i);
        }
        return outputArray;
    } catch (error) {
        console.error('Error decoding base64 string:', error);
        throw error;
    }
}
