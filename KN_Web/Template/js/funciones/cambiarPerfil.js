$.validator.addMethod("extensionImagen", function (value, element) {
    if (element.files.length === 0) return true;
    var extension = element.files[0].name.split(".").pop().toLowerCase();
    return ["png"].includes(extension);
}, "Solo se permiten archivos .png");

$.validator.addMethod("tamanoMaximo", function (value, element, maxMB) {
    if (element.files.length === 0) return true;
    return element.files[0].size <= maxMB * 1024 * 1024;
}, "El archivo no puede superar 1 MB.");

$(function () {
    $("#FormPerfil").validate({
        rules: {
            Identificacion: {
                required: true
            },
            Nombre: {
                required: true
            },
            CorreoElectronico: {
                required: true,
                email: true
            },
            ImagenUsuario: {
                extensionImagen: true,
                tamanoMaximo: 1
            }
        },
        messages: {
            Identificacion: {
                required: "Campo requerido"
            },
            Nombre: {
                required: "Campo requerido"
            },
            CorreoElectronico: {
                required: "Campo requerido",
                email: "Formato incorrecto"
           
           
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


function ConsultarNombre() {

    //$("#Nombre").val("");
    document.getElementById("Nombre").value = "";

    let identificacion = document.getElementById("Identificacion").value;

    if (identificacion.length >= 9) {
        $.ajax({
            url: `https://apis.gometa.org/cedulas/${identificacion}`,
            type: 'GET',
            dataType: 'json',
            success: function (response) {

                if (response?.results?.[0]?.fullname)
                    document.getElementById("Nombre").value = response.results[0].fullname;

            }
        });
    }

}