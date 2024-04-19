async function setupContentLoader(buttonSelector, contentSelector, url) {
    $(document).ready(function () {
        $(buttonSelector).click(function () {
            $(contentSelector).empty();
            $(contentSelector).load(url);
            jQuery('html,body').animate({ scrollTop: 0 }, 0);
        });
    });
}
async function addToBasket(productId) {
    event.preventDefault();
    await fetch("/Basket/AddToBasket", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.reload();
};
async function removeFromBasket(productId) {
    event.preventDefault();
    await fetch("/Basket/RemoveFromBasket", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.reload();
};
async function addToFavorites(productId) {
    event.preventDefault();
    await fetch("/Favorites/AddToFavorites", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.reload();
};
async function removeFromFavorites(productId) {
    event.preventDefault();
    await fetch("/Favorites/RemoveFromFavorites", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.reload();
};
async function deleteNotification(notificationId) {
    event.preventDefault();
    const response = await fetch("/Notifications/DeleteNotification", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(notificationId)
    });
    window.location.pathname = "/Profile/ShowNotifications";
}
async function buyProduct(productId) {
    event.preventDefault();
    const response = await fetch("/Basket/AddToBasket", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.pathname = "/Basket/Index";
};