var users = [];
var orders = [];
var selectedUserId = 0;
var loadUsers = async () => {
    const response = await fetch("/api/users/names", {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Cache-Control': 'no-cache'
        }
    });

    users = await response.json();
}

function removeAllChildNodes(element) {
    while (element.firstChild) {
        element.removeChild(element.firstChild);
    }
}

function formatDate(date) {
    return date.split('T')[0];
}

async function loadOrderDetails() {

}

async function loadOrders() {
    const response = await fetch("/api/orderDetails/" + selectedUserId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    });

    var result = await response.json();
    if (result.orders != null)
        orders = result.orders;
}

function createOrderElement(order) {
    var orderElement = document.createElement('a');
    orderElement.href = '#';
    orderElement.className = 'list-group-item list-group-item-action';
    var divElement = document.createElement('div');
    divElement.className = "d-flex w-100";
    var spanElement = document.createElement('span');
    spanElement.innerText = 'Order Amount: ' + order.orderAmount;
    divElement.appendChild(spanElement);
    spanElement = document.createElement('span');
    spanElement.className = 'ml-auto';
    spanElement.innerText = 'Order Date: ' + formatDate(order.orderDate.toString());
    divElement.appendChild(spanElement);
    orderElement.appendChild(divElement);
    return orderElement;
}

var renderOrders = function () {
    var ordersDiv = document.getElementById("orders");
    removeAllChildNodes(ordersDiv);
    for (let index = 0; index < orders.length; index++) {
        var orderElement = createOrderElement(orders[index]);
        ordersDiv.appendChild(orderElement);
    }
}

async function getOrders(userElement) {
    orders = [];
    selectedUserId = userElement.attributes['data-userId'];
    await loadOrders();
    renderOrders();
}

async function addOrder() {
    var amount = document.getElementById('amount').value;
    var orderDate = document.getElementById('orderDate').value;
    var order = {
        orderId: 0, orderAmount: amount, orderDate: orderDate
    };

    var response = await fetch('api/user/' + selectedUserId + '/orders', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(order)
    });

    var result = await response.json();
    await loadOrders();
    renderOrders();
}

async function addUser() {
    var name = document.getElementById('userName').value;
    var age = document.getElementById('age').value;
    var email = document.getElementById('email').value;
    var user = {
        name: name, age: age, email: email
    };

    var response = await fetch('api/users', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(user)
    });

    //var result = await response.json();
    await loadUsers();
    renderUsers();
    alert('User added');
}

function createUserElement(user) {
    var userElement = document.createElement('a');
    userElement.href = '#';
    userElement.className = 'list-group-item list-group-item-action';
    userElement.innerText = user.name;
    userElement.attributes['data-userId'] = user.userId;
    userElement.onclick = async() => await getOrders(userElement);
    return userElement;
}

var renderUsers = async function () {
    var usersDiv = document.getElementById("users");
    removeAllChildNodes(usersDiv);
    if (users.length > 0)
        selectedUserId = users[0].userId;
    for (let index = 0; index < users.length; index++) {
        var userElement = createUserElement(users[index]);
        usersDiv.appendChild(userElement);
    }

    await loadOrders();
    renderOrders();
}

window.onload = async () => {
    await loadUsers();
    renderUsers();
};