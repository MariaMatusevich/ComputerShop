function InStockSetIdToSellModal(id) {
    document.getElementById('modalId').value = id;
}

function CatalogSetIdToSellModal(id) {
    document.getElementById('modalId').value = id;
}


$(document).ready(function () {
    $('#buttonSell').click(function () {
        var data = { //Fetch form data
            'Destination': $('input[name=Destination]').val(),
            'EquipmentId': $('#modalId').val()
        };
        $.ajax({
            url: 'Sell',
            type: "POST",
            data: data,
            success: function (response) {
                if (response.Type == 200)
                {
                    document.getElementById('modalMessage').innerHTML = response.Message;
                    $('#myModal').modal('hide');
                    $('#mySmallModal').modal('show');
                    setTimeout(function () {
                        $('#mySmallModal').modal('hide');
                        window.location.reload();
                    }, 4000);
                }
            },
            error: function (response) {
                alert(response.Message);
            },
            dataType: 'json'
        });
    })

    $('#buttonBuy').click(function () {
        var data = { //Fetch form data
            'Destination': $('input[name=Destination]').val(),
            'EquipmentId': $('#modalId').val()
        };
        $.ajax({
            url: 'Buy',
            type: "POST",
            data: data,
            success: function (response) {
                if (response.Type == 200) {
                    document.getElementById('modalMessage').innerHTML = response.Message;
                    $('#myModal').modal('hide');
                    $('#mySmallModal').modal('show');
                    setTimeout(function () {
                        $('#mySmallModal').modal('hide');
                        window.location.reload();
                    }, 4000);
                }
            },
            error: function (response) {
                alert(response.Message);
            },
            dataType: 'json'
        });
    })
});
