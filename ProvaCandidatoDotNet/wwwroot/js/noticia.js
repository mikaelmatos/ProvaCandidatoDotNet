$(function () {
    $('#btnCreate').click(function () {
        $.get('/Noticia/Create', function (html) {
            $('#modalBody').html(html);
            $('#modalNoticia').modal('show');
        });
    });

});
function editarNoticia(id) {
    $.get('/Noticia/Edit/' + id, function (html) {
        $('#modalBody').html(html);
        $('#modalNoticia').modal('show');
    });
}

function salvarNoticia() {
    var form = $('#formNoticia');
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (result) {
            if (result.success) {
                $('#modalNoticia').modal('hide');
                location.reload();
            } else {
                $('#modalBody').html(result);
            }
        },
        error: function () {
            alert('Erro ao salvar notícia');
        }
    });
    return false;
}
