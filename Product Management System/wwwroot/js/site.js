function deleteProduct(product_Id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You want to delete this product!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'Product/DeleteProduct',
                type: 'POST',
                data: { product_Id: product_Id },
                success: function (response) {
                    if (response === true) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Product has been deleted.",
                            icon: "success"
                        }).then(() => {
                            location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Something went wrong.",
                            icon: "error"
                        });
                    }
                },
                error: function (error) {
                    Swal.fire({
                        title: "Error!",
                        text: "Something went wrong.",
                        icon: "error"
                    });
                }
            });
        }
    });
}


$(document).ready(function () {
    $('#productList').DataTable({
        language: {
            searchPlaceholder: "Search here"
        },
        "lengthMenu": [[5, 10, 20, 50, 100 - 1], [5, 10, 20, 50, 100, "All"]],
        "pageLength": 5
    });

});


$(document).ready(function () {
    $('#userListTable').DataTable({
        language: {
            searchPlaceholder: "Search here"
        },
        "lengthMenu": [[5, 10, 20, 50, 100 - 1], [5, 10, 20, 50, 100, "All"]],
        "pageLength": 5
    });

});


function viewProductDetails(Id) {
    window.location.href = `Shop/Details?Id=${Id}`;
}



function deleteUser(User_Id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You want to delete this user!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'Admin/Delete',
                type: 'POST',
                data: { Id: User_Id },
                success: function (response) {
                    if (response === true) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "User has been deleted.",
                            icon: "success"
                        }).then(() => {
                            location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Something went wrong.",
                            icon: "error"
                        });
                    }
                },
                error: function (error) {
                    Swal.fire({
                        title: "Error!",
                        text: "Something went wrong.",
                        icon: "error"
                    });
                }
            });
        }
    });
}


function addToCart(product_Id) {
    var quantityInput = document.getElementById('quantityInput');
    var quantityvalue = parseInt(quantityInput.value);
    $.ajax({
        url: 'addToCart',
        type: 'POST',
        data: { product_Id: product_Id, quantity: quantityvalue },
        success: function (response) {
            if (response === true) {
                Swal.fire({
                    title: "Success!",
                    text: 'This product has been add to cart.',
                    icon: "success"
                }).then(() => {
                    location.reload();
                });
            } else {
                Swal.fire({
                    title: "Error!",
                    text: "Something went wrong.",
                    icon: "error"
                });
            }
        },
        error: function (error) {
            Swal.fire({
                title: "Error!",
                text: "Something went wrong.",
                icon: "error"
            });
        }
    });
}


function removeCart(cart_Id) {
    $.ajax({
        url: 'Cart/RemoveFromCart',
        type: 'POST',
        data: { Id: cart_Id},
        success: function (response) {
            window.location.reload();
        },
        error: function (error) {
            Swal.fire({
                title: "Error!",
                text: "Something went wrong.",
                icon: "error"
            });
        }
    });
}