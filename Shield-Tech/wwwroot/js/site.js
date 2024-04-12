// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Navbar Animation
function markActiveLink(element) {
    // Remove active class from all links
    const links = document.querySelectorAll('.nav-link');
    links.forEach(link => link.classList.remove('active'));

    // Add active class to the clicked link
    element.classList.add('active');
}


//Add to cart
document.querySelectorAll('.buy-now').forEach(button => {
    button.addEventListener('click', function (event) {
         // Prevent default button behavior
        event.preventDefault();

        console.log("cliked")
        const productId = this.dataset.productId;
        const productName = this.dataset.productName;
        const productPrice = this.dataset.productPrice;

        const product = {
            id: productId,
            name: productName,
            price: productPrice
        };

        let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
        cartItems.push(product);
        localStorage.setItem('cartItems', JSON.stringify(cartItems));

        // Optionally, you can display a message or update the UI to indicate the product has been added to the cart
        alert('Product added to cart!');

     
    });
});

// Function to display cart items in the modal
function displayCartItems() {
    const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    const cartItemsContainer = document.getElementById('cartItems');
    const totalAmountElement = document.getElementById('totalAmount');
    let totalAmount = 0; // Initialize total amount

    cartItemsContainer.innerHTML = '';

    cartItems.forEach(item => {
        const cartItemDiv = document.createElement('div');
        cartItemDiv.classList.add('mb-3');
        cartItemDiv.innerHTML = `
            <div class="d-flex justify-content-between align-items-center">
                <span>${item.name}</span>
                <span>${item.price}</span>
                <div class="btn-group">
                    <button type="button" class="btn btn-sm btn-secondary" onclick="removeItem('${item.id}')">Remove</button>
                    <button type="button" class="btn btn-sm btn-primary" onclick="increaseQuantity('${item.id}')">+</button>
                </div>
            </div>
        `;
        cartItemsContainer.appendChild(cartItemDiv);

        // Calculate total amount
        totalAmount += parseFloat(item.price);
    });

    // Display total amount
    totalAmountElement.textContent = totalAmount.toFixed(2); // Assuming price is in decimal format
}

// Function to remove item from cart
function removeItem(itemId) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

    // Filter out the item with the specified itemId
    cartItems = cartItems.filter(item => item.id !== itemId);

    // Update the cartItems in local storage
    localStorage.setItem('cartItems', JSON.stringify(cartItems));

    // Refresh the cart items display
    displayCartItems();
}

// Function to increase quantity of item in cart
function increaseQuantity(itemId) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

    // Find the item with the specified itemId
    const item = cartItems.find(item => item.id === itemId);

    // Increase quantity (assuming there's a quantity property in the item object)
    if (item) {
        item.quantity = (item.quantity || 1) + 1; // Increase quantity by 1
    }

    // Update the cartItems in local storage
    localStorage.setItem('cartItems', JSON.stringify(cartItems));

    // Refresh the cart items display
    displayCartItems();
}

// Display cart items when the modal is shown
document.getElementById('cartModal').addEventListener('shown.bs.modal', displayCartItems);




// slide show
var slideIndex = 0;
showSlides();

function showSlides() {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 5000); // Change slide every 5 seconds
}

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}s