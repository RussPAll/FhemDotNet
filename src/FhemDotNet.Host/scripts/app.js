var fhemDotNet = angular.module('fhemDotNet', [])
    .directive('slider', function () {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                element.slider({
                    min: 6,
                    max: 29,
                    value: 10
                });
            }
        };
    });