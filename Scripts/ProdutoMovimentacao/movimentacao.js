$(document).ready(function () {
    $("#quantidade").keyup(function () {
        updateEstoqueFinal();
    });

    $("#tipo").change( function () {
        updateEstoqueFinal();
    });

    $('#btnDelete').click(fnOpenNormalDialog);
});

function updateEstoqueFinal()
{
    var estoqueFinal = 0.00;
    var quantidade = ($("#quantidade").val()) ? parseFloat($("#quantidade").val()) : 0;
    estoqueFinal = ($("#tipo").val() == '+') ? parseFloat($("#estoque_atual").val()) + quantidade : parseFloat($("#estoque_atual").val()) - quantidade;
    $("#estoque").val(estoqueFinal);
}

function fnOpenNormalDialog() {
    $("#dialog-confirm").html("Tem certeza que deseja deletar o produto?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Modal",
        height: 250,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                callback(true);
            },
            "No": function () {
                $(this).dialog('close');
                callback(false);
            }
        }
    });
}

function callback(value) {
    if (value) {
        window.location.replace("http://localhost:53119/ProdutoMovimentacao/Produto/DeleteAction/" + $("#id").val());
    }
}