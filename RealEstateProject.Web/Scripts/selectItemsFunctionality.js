$(document).ready(function () {
    $(".pageCountSelection, .orderSelection").change(function () {
        $.get("Property/PartialIndex?pageSize=" + $(".pageCountSelection").val() + "&" + "sortOrder=" + $(".orderSelection").val(), function (data, status) {
            $("#content").html(data);
            history.replaceState(null, null, "/Property/Index?" + "page=" + $('.active').text() + "&" + "pageSize=" + $(".pageCountSelection").val() + "&" + "sortOrder=" + $(".orderSelection").val());
        });
    });
});


$(document).ready(function () {
    $("#searchButton").click(function () {
        $.get("PROPERTY/PartialIndex?searchString=" + $("#searchString").val(), function (data, status) {
            var resultData = $(data).find(".propertiesList");
            $(".PropTableStyles").html(resultData);
            var type = $(data).find(".atoph_seo").text();
            type = type.trim();
            $(".typeSelection").val(type);
        });
    });
});

$(document).on("click", "#contentPager a[href]", function () {
    if ($(this).attr('href')) {
        $.ajax({
            url: $(this).attr("href") + "&" + "pageSize=" + $(".pageCountSelection").val() + "&" + "sortOrder=" + $(".orderSelection").val(),
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#content').html(result);
                history.pushState(null, null, "/Property/Index?" + "page=" + $('.active').text() + "&" + "pageSize=" + $(".pageCountSelection").val() + "&" + "sortOrder=" + $(".orderSelection").val());
            }
        });
    }
    return false;
});




$(document).ready(function () {
    $(".imageoflist").click(function () {
        $(".imagemain").attr("src", $(this).attr("src"));
    });
});

$(".cities").change(function () {
    $.get("RegionSearch?cityID=" + $(".cities").val(), function (data, status) {
        var result = $(data).find("option");
        $(".regions").html(result);
    });
});

//------------------------------ SearchCriteria scripts ----------------------------------------//

$(document).ready(function () {
    $(".searchBtn").hover(function () {
        $(this).attr('href', '/Property/SearchCriteriaResult?'
        + "RentFrom=" + $('.RentFrom').val()
        + "&RentTo=" + $('.RentTo').val()
        + "&SellFrom=" + $('.SellFrom').val()
        + "&SellTo=" + $('.SellTo').val()
        + "&SizeFrom=" + $('.SizeFrom').val()
        + "&SizeTo=" + $('.SizeTo').val()
        + "&City=" + $('.cities option:selected').val()
        + "&Region=" + $('.regions option:selected').val()
        + "&propType=" + $('.PropType:checked').map(function () {
            return this.value;
        }).get()
        + "&Features=" + $('.feats:checked').map(function () {
            return this.value;
        }).get())
    });
    return false;
});



$(document).ready(function () {
    $(document).on('change', ".pageCountSelectionPost, .orderSelectionPost", function () {
        $.ajax({
            url: this.value,
            type: 'GET',
            cache: false,
            success: function (data, status) {
                $("#content").html($(data).find("#content"));

                //alert($(data).find('.orderSelectionPost option:contains("' + 'Цена Наем (възходящ)' + '")').attr('value'));
                //alert($(data).find('.orderSelectionPost option:contains("' + 'Цена Наем (възходящ)' + '")').attr('selected'));
                //var blabla = $(data).find('.orderSelectionPost option:contains("' + $('.orderSelectionPost option:selected').text() + '")').prop('selected',true);
                var selectedO = $(data).find('.orderSelectionPost option:contains("' + $('.orderSelectionPost option:selected').text() + '")').text();
                var selectedPC = $(data).find('.pageCountSelectionPost option:contains("' + $('.pageCountSelectionPost option:selected').text() + '")').text();
                //alert(blabla);
                //alert((data).find('.orderSelectionPost').val());
                //alert($('.orderSelectionPost option:selected').html());
                //alert($(data).find(".orderSelectionPost option:selected").html())
                $('.orderSelectionPost').html($(data).find(".orderSelectionPost").html());
                $('.pageCountSelectionPost').html($(data).find(".pageCountSelectionPost").html());
                //alert(selected);
                
                $('.orderSelectionPost option:contains("' + selectedO + '")').prop('selected', true);
                $('.pageCountSelectionPost option:contains("' + selectedPC + '")').prop('selected', true);


                history.replaceState(null, null, this.url);
            }
        });
    });
    return false;
});

$(document).on("click", "#contentPagerPost a[href]", function () {
    $.ajax({
        url: $(this).attr("href"),
        type: 'GET',
        cache: false,
        success: function (data, status) {
            $("#content").html($(data).find("#content"));
            history.replaceState(null, null, this.url);
        }
    });
    return false;
});



$(document).ready(function () {
    var checkedValues = $('feats:checked').map(function () {
        return this.value;
    }).get();
    $(".testdiv").text(checkedValues);
});


$(".toggleBtn").click(function () {
    $("#toggle").toggle("drop");
});


//----------------------------------------------------------------



//$(".typeSelection").change(function () {
//    $.get("Property/Index?pageSize=" + $("#pageCountSelect").val() + "&" + "Type=" + $(".typeSelection").val(), function (data, status) {
//        var resultData = $(data).find("#PropertiesContainer")
//        $("#PropertiesContainer").html(resultData);
//    });
//});


//$(document).ready(function () {
//    $("ul.pagination > li > a").click(function () {
//        $.get("Property/PartialIndex?pageSize=" + $(".pageCountSelection").val() + "&" + "type="
//            + $(".typeSelection").val() + "&" + "sortOrder=" + $(".orderSelection").val(), function (data, status) {
//                var result = $(data).find(".propertiesList");
//                $(".propertiesList").html(result);
//            });
//    });
//});

//$(document).ready(function () {
//    $('body').on('change', ".pageCountSelectionPost, .orderSelectionPost", function () {
//        $.ajax({
//            url: "/Property/SearchCriteriaResult",
//            type: 'GET',
//            cache: false,
//            data: {
//                pageSize: $(".pageCountSelectionPost").val(),
//                sortOrder: $(".orderSelectionPost").val(),
//                page: 1,
//                RentFrom: $('.RentFrom').val(),
//                RentTo: $('.RentTo').val(),
//                SellFrom: $('.SellFrom').val(),
//                SellTo: $('.SellTo').val(),
//                SizeFrom: $('.SizeFrom').val(),
//                SizeTo: $('.SizeTo').val(),
//                City: $('.cities option:selected').val(),
//                Region: $('.regions option:selected').val(),
//                propType: $('.PropType:checked').map(function () {
//                    return this.value;
//                }).get(),
//                Features: $('.feats:checked').map(function () {
//                    return this.value;
//                }).get()
//            }
//            , success: function (data, status) {
//                $("#content").html($(data).find("#content"));
//                history.pushState(null, null, this.url);
//            }
//        });
//    });
//    return false;
//});


//$(document).on("click", "#contentPagerPost a[href]", function () {
//    if ($(this).attr('href')) {
//        $.ajax({
//            url: $(this).attr("href") + "&" + "pageSize=" + $(".pageCountSelectionPost").val() + "&" + "sortOrder=" + $(".orderSelection").val(),
//            type: 'GET',
//            data: {
//                RentFrom: $('.RentFrom').val(),
//                RentTo: $('.RentTo').val(),
//                SellFrom: $('.SellFrom').val(),
//                SellTo: $('.SellTo').val(),
//                SizeFrom: $('.SizeFrom').val(),
//                SizeTo: $('.SizeTo').val(),
//                City: $('.cities option:selected').val(),
//                Region: $('.regions option:selected').val(),
//                propType: $('.PropType:checked').map(function () {
//                    return this.value;
//                }).get().toString(),
//                Features: $('.feats:checked').map(function () {
//                    return this.value;
//                }).get().toString()
//            },
//            cache: false,
//            success: function (result) {
//                $('#content').html($(result).find("#content"));
//                history.pushState(null, null, this.url);
//            }
//        });

//    }

//    return false;
//});



//$(document).ready(function () {
//    $(".searchBtn").click(function () {
//        $.ajax({
//            type: 'GET',
//            url: '/Property/SearchCriteriaResult',
//            data: {
//                RentFrom: $('.RentFrom').val(),
//                RentTo: $('.RentTo').val(),
//                SellFrom: $('.SellFrom').val(),
//                SellTo: $('.SellTo').val(),
//                SizeFrom: $('.SizeFrom').val(),
//                SizeTo: $('.SizeTo').val(),
//                City: $('.cities option:selected').val(),
//                Region: $('.regions option:selected').val(),
//                propType: $('.PropType:checked').map(function () {
//                    return this.value;
//                }).get().toString(),
//                Features: $('.feats:checked').map(function () {
//                    return this.value;
//                }).get().toString()
//            },
//            success: function (data) {
//                //alert($("body").text());
//                history.pushState(null, null, this.url);
//                $("body").html($(data).find(".fullPropertiesListInfo"));

//            },
//            error: function (xhr, ajaxOptions, error) {
//                alert(xhr.status);
//                //alert('Error: ' + xhr.responseText);
//            }
//        });

//        $(".testdiv").text('[' + PSVM.City + ']' + '[' + PSVM.Region + ']' + '[' + PSVM.SizeFrom + '-' + PSVM.SizeTo + ']' + '[' + PSVM.SellFrom + '-' + PSVM.SellTo + ']' + '[' + PSVM.RentFrom + '-' + PSVM.RentTo + ']' + '[' + PSVM.propType + ']' + ' ' + '[' + PSVM.checkedValues + ']');

//    });
//    return false;
//});