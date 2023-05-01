let countCreate = 1;
let countUpdate = 1;
let sizesList = document.getElementById("sizes-list");

function AddSizeRow() {
    sizesList.insertAdjacentHTML("beforeend", `<div>` +
        `<label for="Sizes">Enter ${countCreate + 1} size:</label>` +
        '<p>Size in string:</p>' +
        `<input name="Sizes[${countCreate}].InString" class="form-control" / >` +
        '<p>Size in number:</p>' +
        `<input type="number" name="Sizes[${countCreate}].InNumber" class="form-control" / >` +
        '</div>');
    countCreate++;
}

function AddSizeRowForUpdate(counter) {
    sizesList.insertAdjacentHTML("beforeend", `<div id="select-size-${counter}"` +
        `<label for="Sizes">Enter ${counter + 1} size:</label>` +
        '<p>Size in string:</p>' +
        `<input name="Sizes[${counter}].InString" class="form-control" / >` +
        '<p>Size in number:</p>' +
        `<input type="number" name="Sizes[${counter}].InNumber" class="form-control" / >` +
        `<div class="btn btn-secondary" onclick="DeleteSize(${counter})">Delete size</div>` +
        '</div>');
}