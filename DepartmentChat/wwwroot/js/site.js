const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/mainhubchats")
    .build();

hubConnection.on("ReceiveMessage", function (userName, message) {
    document.getElementById("chatroom").insertAdjacentHTML('beforeend', "<p>" + userName + ": " + message + "</p>");

});

document.getElementById("sendButton").addEventListener("click", function (e) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var room = document.getElementById("room").value;
    hubConnection.invoke("SendMessage", user, message, room, false);
});

document.getElementById("joinButton").addEventListener("click", function (e) {
    var room = document.getElementById("room").value;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    hubConnection.invoke("SendMessage", user, message, room, true);
});


hubConnection.start();