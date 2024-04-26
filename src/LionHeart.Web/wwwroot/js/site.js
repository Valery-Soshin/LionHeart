
const BASKET_CREATE_ORDER = "/Basket/CreateOrder";
const BASKET_ADD_TO_BASKET = "/Basket/AddToBasket";
const BASKET_REMOVE_FROM_BASKET = "/Basket/RemoveFromBasket";
const BASKET_UPDATE_QUANTITY = "/Basket/UpdateQuantity";

const FAVORITES_ADD_TO_FAVORITES = "/Favorites/AddToFavorites";
const FAVORITES_REMOVE_FROM_FAVORITES = "/Favorites/RemoveFromFavorites";

const NOTIFICATIONS_DELETE_NOTIFICATION = "/Notifications/DeleteNotification";

const PROFILE_SHOW_NOTIFICATIONS = "/Profile/ShowNotifications";


async function addToBasket(productId) {
    event.preventDefault();
    await fetch(BASKET_ADD_TO_BASKET, {
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
    await fetch(BASKET_REMOVE_FROM_BASKET, {
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
    await fetch(FAVORITES_ADD_TO_FAVORITES, {
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
    await fetch(FAVORITES_REMOVE_FROM_FAVORITES, {
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
    const response = await fetch(NOTIFICATIONS_DELETE_NOTIFICATION, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(notificationId)
    });
    window.location.pathname = PROFILE_SHOW_NOTIFICATIONS;
}
async function buyProduct(productId) {
    event.preventDefault();
    const response = await fetch(BASKET_ADD_TO_BASKET, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productId)
    });
    window.location.pathname = BASKET_CREATE_ORDER;
};
async function updateQuantity(id, quantity) {
    await fetch(BASKET_UPDATE_QUANTITY, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            entryid: id,
            productquantity: quantity
        })
    });
};