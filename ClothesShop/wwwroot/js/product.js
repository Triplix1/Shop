function ValidateInput() {
    if (document.getElementById("uploadBox").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please upload an Image!',
        });
        return false;
    }
    return true;
}

function GetSizesList() {
    let categorySelector = document.getElementById("select-category");
    let sizesSelector = document.getElementById("select-sizes");
    if (categorySelector.value != "") {
        fetch(`/api/sizes-list/${categorySelector.value}`, {
            method: "GET"
        }).then((response) => response.json())
            .then((data) => console.log(data));
    }
}