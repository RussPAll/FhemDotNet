fhemDotNet.directive('uiSlider', ['ui.config', function (uiConfig) {
    'use strict';
    var options;
    options = {};
    if (angular.isObject(uiConfig.slider)) {
        angular.extend(options, uiConfig.slider);
    }
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, controller) {
            var getOptions = function () {
                return angular.extend({}, uiConfig.slider, scope.$eval(attrs.uiSlider));
            };
            var initSliderWidget = function () {
                var opts = getOptions();

                if (controller) {
                    var updateModel = function () {
                        scope.$apply(function () {
                            var value = element.slider("value");
                            controller.$setViewValue(value);
                            element.blur();
                        });
                    };

                    //                    if (opts.onSlide) {
                    //                        // Caller has specified onSelect, so call this as well as updating the model
                    //                        var userOnSlide = opts.onSlide;
                    //                        opts.slide = function(event, ui) {
                    //                            updateModel();
                    //                            scope.$apply(function() {
                    //                                userOnSlide(event, ui);
                    //                            });
                    //                        };
                    //                    } else {
                    //                        // No onSelect already specified so just update the model
                    //                        opts.slide = updateModel;
                    //                    }

                    if (opts.onChange) {
                        // Caller has specified onSelect, so call this as well as updating the model
                        var userOnChange = opts.onChange;
                        opts.change = function (event, ui) {
                            updateModel();
                            scope.$apply(function () {
                                userOnChange(event, ui);
                            });
                        };
                    }

                    // Update the slider when the model changes
                    controller.$render = function () {
                        var value = controller.$viewValue;
                        element.slider("value", value);
                    };
                }

                // element.slider('destroy
                element.attr("value", controller.$viewValue);
                element.slider(opts);

                if (controller) {
                    controller.$render();
                }
            };
            scope.$watch(getOptions, initSliderWidget, true);
        }
    };
} ]);