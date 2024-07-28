// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });

console.log("Service worker loaded");

self.addEventListener('push', function (event) {
    console.log('Push event received:', event);

    let data = {};
    if (event.data) {
        data = event.data.json();
    }

    console.log('Push event data:', data);

    const options = {
        body: data.body || 'Default body',
        icon: 'icon.png', // Path to the icon image
        badge: 'badge.png' // Path to the badge image
    };

    event.waitUntil(
        self.registration.showNotification(data.title || 'Default title', options)
    );
});

self.addEventListener('notificationclick', function (event) {
    event.notification.close();
    event.waitUntil(
        clients.openWindow('https://your-website.com') // Adjust this URL to where you want the user to go when they click the notification
    );
});


