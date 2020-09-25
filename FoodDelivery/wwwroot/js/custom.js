$(document).ready(function () {
    $(window).scroll(function () {
        var height = $(window).scrollTop();
        if (height > 100) {
            $('#button').fadeIn();
        } else {
            $('#button').hide();
        }
    });
    $("#button").click(function () {
        $('html, body').animate({
            scrollTop: $("#header").offset().top
        }, 2000);
    });

    // Handle Sign Up
    //Btn Sign Up Call
    $("#btnNext").on("click", function () {
        var url = "/Account/SignUp";
        var UserType = $("input[name='radio-group']:checked").val();
       var data= $("#btnNext").hasClass("isDisabled");
        if (!data) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { UserType: UserType },
                success: function (response) {
                    $("#PartialVies").html(response);
                },
                error: function () {
                }
            });
        }
    });




    //Btn Sign Up Call
    $("#btnSignUp").on("click", function () {
        window.location.href = "/Account/HandleSignUp";
    });

    //Btn Login Call
    $("#btnLogin").on("click", function () {
        debugger
        var url = "/Account/SignIn";
        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                $("#exampleModalLongTitle").html("Sign In");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {

            }
        });

    });

});

$(document).ready(function () {
    var modal = document.getElementById("apmodal");
    var btn = document.getElementById("myBtn");
    var span = document.getElementsByClassName("close")[0];
    btn.onclick = function () {
        modal.style.display = "block";
    }

    span.onclick = function () {
        modal.style.display = "none";
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});

$(document).ready(function () {

    $(".input_dr").on("focus", function () {
        $(".current_location").removeClass("d-none");
    });

    $(".input_dr").focusout(function () {
        $(".current_location").addClass("d-none");
    })

    $(".input_dr1").on("focus", function () {
        $(".current_location1").removeClass("d-none");
    });

    $(".input_dr1").focusout(function () {
        $(".current_location1").addClass("d-none");
    })

});


