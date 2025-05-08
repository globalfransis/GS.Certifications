// JavaScript source code
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.on("ReceiveNotification", function (message) {
    console.log(message);
    alert(message);
});

connection.start().then(function () {
    console.log("Connected to notification Hub.")
}).catch(function (err) {
    return console.error(err.toString());
});