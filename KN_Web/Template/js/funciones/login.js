//esto valida el formulario de login, asegurándose de que los campos de identificación y contraseña no estén vacíos antes de enviar el formulario. 
//Si un campo está vacío, se muestra un mensaje de error debajo del campo correspondiente y se resalta el campo con una clase CSS
//para indicar que es inválido.Cuando el usuario corrige el error, la clase de error se elimina y el mensaje de error desaparece.

$(function () {
    $("#FormLogin").validate({
        rules: {
            CorreoElectronico: {
                required: true,
                email: true
            },
            Contrasena: {
                required: true
            }
        },
        messages: {
            CorreoElectronico: {
                required: "Campo requerido",
                email: "Ingrese un correo electrónico válido"
            },
            Contrasena: {
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

