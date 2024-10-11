$(document).ready(function () {
    $(".sidebar-burgur").click(function () {
        $(".sidebar,.t-header").toggleClass("shring");
        $(".sidebar").css("width", "4rem", "max-width", "4rem", "min-width", "4rem"), 500;
        $(".page-body").toggleClass("explodeBody");
    });
    $(".ingnitsubmenu").click(function () {
        //$(".sidebar,.t-header").removeClass("shring");
        ////$(".sidebar").css("width","4rem","max-width","4rem","min-width","4rem"), 500 ;
        //$(".page-body").removeClass("explodeBody");
    });

});

$(document).ready(function () {
    //$('.dataTable').DataTable({
    //    scrollY: '34vh',
    //    scrollX: true,
    //    scrollCollapse: true,
    //    paging: true,

    //    //bSort : false,
    //    "language": {
    //        "emptyTable": "No record available"
    //    },
    //    fixedColumns: {
    //        right: 1
    //    },
    //    "columnDefs": [
    //        { "className": "text-center", "targets": "_all" }

    //    ],
    //    fixedColumns: true
    //});

    //$(".dataTfullheight").parent(".dataTables_scrollBody").css("max-height", "50vh")

    // site.js

    // Common DataTable initialization
    function initializeDataTable(selector, order = []) {
        $(selector).DataTable({
            scrollY: '34vh',
            scrollX: true,
            scrollCollapse: true,
            paging: true,
            "language": {
                "emptyTable": "No record available"
            },
            fixedColumns: {
                right: 1
            },
            columnDefs: [
                { "className": "text-center", "targets": "_all" }
            ],
            headerCallback: function (thead, data, start, end, display) {
                capitalizeTableHeaders(thead);
            },
            order: order
        });

        $(selector).parent(".dataTables_scrollBody").css("max-height", "50vh");
    }

    // Function to capitalize table headers
    function capitalizeTableHeaders(thead) {
        var columnHeaders = $('th', thead);
        columnHeaders.each(function (index, element) {
            var headerText = $(element).text();
            $(element).text(headerText.toUpperCase());
        });
    }

    // Initialize DataTable with no default order
    $(document).ready(function () {
        initializeDataTable('.dataTable');
    });


});




/*------------------------table resize click on left menu close----------------------*/

$(document).ready(function () {
    $('.sidebar-burgur').on('click', function (e) {
        // var target = $(e.target).attr("href"); // activated tab
        // alert (target);
        $($.fn.dataTable.tables(true)).css('width', '100%');
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw();
    });
});
//Tooltip
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

      // Created by @conmarap.

    Array.prototype.search = function (elem) {
      for (var i = 0; i < this.length; i++) {
        if (this[i] == elem) return i;
      }

    return -1;
    };

    var Multiselect = function (selector) {
      if (!$(selector)) {
        console.error("ERROR: Element %s does not exist.", selector);
    return;
      }

    this.selector = selector;
    this.selections = [];

    (function (that) {
        that.events();
      })(this);
    };

    Multiselect.prototype = {
        open: function (that) {
        var target = $(that).parent().attr("data-target");

    // If we are not keeping track of this one's entries, then
    // start doing so.
    if (!this.selections) {
        this.selections = [];
        }

    $(this.selector + ".multiselect").toggleClass("active");
      },

    close: function () {
        $(this.selector + ".multiselect").removeClass("active");
      },

    events: function () {
        var that = this;

    $(document).on(
    "click",
          that.selector + ".multiselect > .title",
    function (e) {
            if (e.target.className.indexOf("close-icon") < 0) {
        that.open();
            }
          }
    );

    $(document).on(
    "click",
    that.selector + ".multiselect option",
    function (e) {
            var selection = $(this).attr("value");
    var target = $(this).parent().parent().attr("data-target");

    var io = that.selections.search(selection);

    if (io < 0) that.selections.push(selection);
    else that.selections.splice(io, 1);

    that.selectionStatus();
    that.setSelectionsString();
          }
    );

    $(document).on(
    "click",
          that.selector + ".multiselect > .title > .close-icon",
    function (e) {
        that.clearSelections();
          }
    );

    $(document).click(function (e) {
          if (e.target.className.indexOf("title") < 0) {
            if (e.target.className.indexOf("text") < 0) {
              if (e.target.className.indexOf("-icon") < 0) {
                if (
    e.target.className.indexOf("selected") < 0 ||
    e.target.localName != "option"
    ) {
        that.close();
                }
              }
            }
          }
        });
      },

    selectionStatus: function () {
        var obj = $(this.selector + ".multiselect");

    if (this.selections.length) obj.addClass("selection");
    else obj.removeClass("selection");
      },

    clearSelections: function () {
        this.selections = [];
    this.selectionStatus();
    this.setSelectionsString();
      },

    getSelections: function () {
        return this.selections;
      },

    setSelectionsString: function () {
        var selects = this.getSelectionsString().split(", ");
        $(this.selector + ".multiselect > .title").attr("title", selects);

    var opts = $(this.selector + ".multiselect option");

        if (selects.length > 6) {
          var _selects = this.getSelectionsString().split(", ");
    _selects = _selects.splice(0, 6);
          $(this.selector + ".multiselect > .title > .text").text(
    _selects + " [...]"
    );
        } else {
        $(this.selector + ".multiselect > .title > .text").text(selects);
        }

    for (var i = 0; i < opts.length; i++) {
        $(opts[i]).removeClass("selected");
        }

    for (var j = 0; j < selects.length; j++) {
          var select = selects[j];

    for (var i = 0; i < opts.length; i++) {
            if ($(opts[i]).attr("value") == select) {
        $(opts[i]).addClass("selected");
    break;
            }
          }
        }
      },

    getSelectionsString: function () {
        if (this.selections.length > 0) return this.selections.join(", ");
    else return "Select";
      },

    setSelections: function (arr) {
        if (!arr[0]) {
        error("ERROR: This does not look like an array.");
    return;
        }

    this.selections = arr;
    this.selectionStatus();
    this.setSelectionsString();
      }
    };

    $(document).ready(function () {
        var multi = new Multiselect("#services");

        $(function () {
            $('.selectmultiple').each(function () {
                $(this).select2({
                    theme: 'bootstrap4',
                    width: 'style',
                    placeholder: $(this).attr('placeholder'),
                    allowClear: Boolean($(this).data('allow-clear')),
                    templateSelection: formatSelection,
                    templateResult: formatResult
                });
            });

            function formatSelection(option) {
                if (!option.id) {
                    return option.text;
                }
                return $('<span><i class="fas fa-caret-down"></i> ' + option.text + '</span>');
            }

            function formatResult(option) {
                if (!option.id) {
                    return option.text;
                }
                return $('<span><i class="fas fa-caret-down"></i> ' + option.text + '</span>');
            }
        });

    });


