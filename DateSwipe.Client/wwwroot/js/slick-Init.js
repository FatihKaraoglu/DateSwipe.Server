window.initializeSlick = () => {
    console.log("Initializing Slick slider...");
    $('.slick-slider').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        dots: true,
        infinite: true,
        speed: 300,
        arrows: true,
        nextArrow: '<button type="button" class="slick-next">Next</button>',
        prevArrow: '<button type="button" class="slick-prev">Prev</button>',
    });
};

