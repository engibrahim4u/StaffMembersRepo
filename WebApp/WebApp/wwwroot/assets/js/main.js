(function($) {
    let togglePassword = $(".toggle-password");

    if ($(".toggle-password").hasClass("zmdi-eye-off"))
      $("#password").attr("type", "password");

    $(".toggle-password").click(function() {

        $(this).toggleClass("zmdi-eye zmdi-eye-off");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
          input.attr("type", "text");
        } else {
          input.attr("type", "password");
        }
      });

})(jQuery);