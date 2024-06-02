function showToast(message, type) {
    var toastContainer = document.getElementById("toastContainer");
    var toast = document.createElement("div");
    toast.className = `toast align-items-center text-white bg-${type} border-0`;
    toast.role = "alert"; //atrybuty dostępności
    toast.ariaLive = "assertive";
    toast.ariaAtomic = "true";

    var toastBody = document.createElement("div");
    toastBody.className = "d-flex";

    var toastContent = document.createElement("div");
    toastContent.className = "toast-body";
    toastContent.textContent = message;

    var toastButton = document.createElement("button");
    toastButton.type = "button";
    toastButton.className = "btn-close btn-close-white me-2 m-auto";
    toastButton.setAttribute("data-bs-dismiss", "toast");
    toastButton.ariaLabel = "Close";

    toastBody.appendChild(toastContent); //złożenie
    toastBody.appendChild(toastButton);
    toast.appendChild(toastBody);

    toastContainer.appendChild(toast);

    var bsToast = new bootstrap.Toast(toast); //tworzy instancję toasta z  Bootstrap i wyświetla 
    bsToast.show();

    setTimeout(function () {
        toast.remove();
    }, 5000);
}

document.addEventListener("DOMContentLoaded", function () {
    var tempDataMessage = document.getElementById("tempDataMessage");
    if (tempDataMessage) {
        showToast(tempDataMessage.textContent, tempDataMessage.getAttribute("data-type"));
        tempDataMessage.remove(); 
    }
});
