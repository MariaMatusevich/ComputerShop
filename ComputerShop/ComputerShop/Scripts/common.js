function InStockSetIdToSellModal(id) {
    document.getElementById('modalId').value = id;
}

function CatalogSetIdToSellModal(id) {
    document.getElementById('modalId').value = id;
}


$(document).ready(function () {
    $('#buttonSell').click(function () {

        if ($('input[name=Destination]').val() == "")
        {
            alert("Введите, пожалуйста, ваш E-mail.");
            return;
        }

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
                    //setTimeout(function () {
                    //    $('#mySmallModal').modal('hide');
                    //    window.location.reload();
                    //}, 4000);
                }
            },
            error: function (response) {
                alert(response.Message);
            },
            dataType: 'json'
        });
    })

    $('.btnModalClose').click(function () {
        //window.location.reload();
        var guid = document.getElementById("modalId").value;
        document.getElementById("modalId").value = "";
        var el = document.getElementById(guid);
        el.parentNode.removeChild(el);
        document.getElementsByName("Destination")[0].value = "";
    });

    $('#buttonBuy').click(function () {

        if ($('input[name=Destination]').val() == "") {
            alert("Введите, пожалуйста, ваш E-mail.");
            return;
        }

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
                    //setTimeout(function () {
                    //    $('#mySmallModal').modal('hide');
                    //    window.location.reload();
                    //}, 4000);
                }
            },
            error: function (response) {
                alert(response.Message);
            },
            dataType: 'json'
        });
    })
});


(function ($) {
    var defaults = {
        callback: function () { },
        runOnLoad: true,
        frequency: 100,
        previousVisibility: null
    };

    var methods = {};
    methods.checkVisibility = function (element, options) {
        if (jQuery.contains(document, element[0])) {
            var previousVisibility = options.previousVisibility;
            var isVisible = element.is(':visible');
            options.previousVisibility = isVisible;
            if (previousVisibility == null) {
                if (options.runOnLoad) {
                    options.callback(element, isVisible);
                }
            } else if (previousVisibility !== isVisible) {
                options.callback(element, isVisible);
            }

            setTimeout(function () {
                methods.checkVisibility(element, options);
            }, options.frequency);
        }
    };

    $.fn.visibilityChanged = function (options) {
        var settings = $.extend({}, defaults, options);
        return this.each(function () {
            methods.checkVisibility($(this), settings);
        });
    };
})(jQuery);