
function SetupGetCustomers(url) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                var customers = JSON.parse(xhr.responseText);
                var container = document.getElementById('customerListContainer');
                container.innerHTML = '';

                var ul = document.createElement('ul');
                ul.classList.add('list-group');

                customers.forEach(function (customer) {
                    var li = document.createElement('li');
                    li.classList.add('list-group-item', 'd-flex', 'justify-content-between', 'align-items-center');

                    var span = document.createElement('span');
                    span.textContent = `${customer.name} (${customer.email})`;

                    var div = document.createElement('div');

                    var editBtn = document.createElement('button');
                    editBtn.classList.add('btn', 'btn-primary', 'mr-2');
                    editBtn.textContent = 'Edit';
                    editBtn.addEventListener('click', function () {
                        window.location.href = '/Home/Edit?id=' + customer.id;
                    });

                    div.appendChild(editBtn);

                    var deleteBtn = document.createElement('button');
                    deleteBtn.classList.add('btn', 'btn-danger', 'ms-2');
                    deleteBtn.textContent = 'Delete';
                    deleteBtn.addEventListener('click', function () {
                        var deleteXhr = new XMLHttpRequest();
                        deleteXhr.open('DELETE', url + '/' + customer.id);
                        deleteXhr.onreadystatechange = function () {
                            if (deleteXhr.readyState === XMLHttpRequest.DONE && deleteXhr.status === 200) {
                                ul.removeChild(li);
                            }
                        };
                        deleteXhr.send();
                    });

                    div.appendChild(deleteBtn);

                    li.appendChild(span);
                    li.appendChild(div);
                    ul.appendChild(li);
                });

                container.appendChild(ul);
            } else {
                console.error('Error fetching customers:', xhr.statusText);
            }
        }
    };
    xhr.send();
}

function SetupUpdateCustomer(url, id) {
    var form = document.getElementById('editCustomerForm');
    var nameInput = document.getElementById('name');
    var emailInput = document.getElementById('email');
    var addressInput = document.getElementById('address');

    var xhr = new XMLHttpRequest();
    xhr.open('GET', url + '/' + id);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                var customer = JSON.parse(xhr.responseText);
                nameInput.value = customer.name;
                emailInput.value = customer.email;
                addressInput.value = customer.address;
            } else {
                console.error('Error fetching customer details:', xhr.statusText);
            }
        }
    };
    xhr.send();

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        var name = nameInput.value.trim();
        var email = emailInput.value.trim();
        var address = addressInput.value.trim();

        if (!name || !email || !address) {
            alert('Please fill in all fields.');
            return;
        }

        var formData = {
            id: id,
            name: name,
            email: email,
            address: address
        };

        var xhrPut = new XMLHttpRequest();
        xhrPut.open('PUT', url + '/' + id);
        xhrPut.setRequestHeader('Content-Type', 'application/json');
        xhrPut.onload = function () {
            if (xhrPut.status === 200) {
                alert('Customer updated successfully');
            } else {
                console.error('Error updating customer:', xhrPut.statusText);
            }
        };
        xhrPut.onerror = function () {
            console.error('Error updating customer:', xhrPut.statusText);
        };
        xhrPut.send(JSON.stringify(formData));
    });
}

function SetupCreateCustomer(url) {
    document.getElementById('createCustomerForm').addEventListener('submit', function (e) {
        e.preventDefault();

        var nameElement = document.getElementById('name');
        var emailElement = document.getElementById('email');
        var addressElement = document.getElementById('address');

        // Validate name
        var name = nameElement.value.trim();
        if (!name) {
            alert('Please enter a name.');
            nameElement.focus();
            return;
        }   

        // Validate email
        var email = emailElement.value.trim();
        if (!email) {
            alert('Please enter an email address.');
            emailElement.focus();
            return;
        }
        if (!isValidEmail(email)) {
            alert('Please enter a valid email address.');
            emailElement.focus();
            return;
        }

        // Validate address
        var address = addressElement.value.trim();
        if (!address) {
            alert('Please enter an address.');
            addressElement.focus();
            return;
        }

        var formData = {
            name: name,
            email: email,
            address: address
        };

        var xhr = new XMLHttpRequest();
        xhr.open('POST', url, true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                alert('Customer created successfully');

                document.getElementById('name').value = '';
                document.getElementById('email').value = '';
                document.getElementById('address').value = '';
            } else {
                alert('Error creating customer: ' + xhr.statusText);
            }
        };
        xhr.onerror = function () {
            alert('Error creating customer: ' + xhr.statusText);
        };
        xhr.send(JSON.stringify(formData));
    });
}

function isValidEmail(email) {
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}