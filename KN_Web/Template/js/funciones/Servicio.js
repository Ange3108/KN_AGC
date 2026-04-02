$.validator.addMethod("precioValido", function (value, element) {
    return this.optional(element) || /^\d+(,\d{1,2})?$/.test(value);
}, "Ingrese un precio válido.");

$(function () {
    $("#FormServicio").validate({
        rules: {
            Nombre: {
                required: true
            },
            Descripcion: {
                required: true
            },
            Precio: {
                required: true,
                precioValido: true
            },
            Video: {
                required: true
            }
        },
        messages: {
            Nombre: {
                required: "Campo requerido"
            },
            Descripcion: {
                required: "Campo requerido"
            },
            Precio: {
                required: "Campo requerido"
            },
            Video: {
                required: "Campo requerido"
            }
        },
        errorElement: "span",
        errorClass: "text-danger",
      
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        }
    });
});