var fhemDotNet = angular.module('fhemDotNet', [])
    .directive('slider', function () {
        return {
            restrict: 'E',
            link: function (scope, element, attrs) {
                //alert(scope);
                attrs.$observe('value', function (value) {
                    element.slider({
                        min: 6,
                        max: 29,
                        value: value,
                        slide: function (event, ui) {
                            $(ui.handle).closest("td").find("input").val(ui.value);
                        }
                    });
                });
            }
        };
    });