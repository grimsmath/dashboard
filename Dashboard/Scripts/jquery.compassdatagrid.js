/*
 * Compass Datagrid plugin 1.3
 *
 * Copyright (c) 2009 Cristian Graziano
 * http://www.compasswebpublisher.com/jquery/compass-datagrid
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 */

(function ($) {
	$.fn.compassDatagrid = function (options) {
		var opts = $.extend({}, 
			{
				hover: true,
				hoverClass: 'hover',

				selectable: true,
				selectableClass: 'selected',

				checkboxToggle: false,

				sort: true,

				ajax: true,

				striping: true,
				oddClass: 'odd',
				evenClass: 'even',

				resizable: false,
				resizeOpacity: 0.65,
				resizer: '#ctResizer',

				toggle: true,
				toggleHolder: 'toggleHolder',

				pager: true,
				pagerBefore: true,
				pagerAfter: true,

				images: 'images',
				url: '',

				perPage: 15,
				perPageOptions: ["10", "15", "20", "25", "30", "40", "50", "100"],
				
				paramsStart: "?",
				paramsSeparator: "&",
				paramsJoin: "="					
			}, 
		options);
		
		$.fn.getPagerOptions = options;
		
		return this.each(function() {
			var $this = $(this);
			$this.addClass('compassTable');
			
			if (opts.ajax) {
				$this.ctLoadJson(opts);
			}
			
			if (opts.sort) {
				/* Bind sorting logic to rows in thead */
				$('.compassTable th').livequery(function() {

					$(this).click(function() {
					
						if($(this).hasClass('col-0')) {
							return;
						}
						
						var thisClass = $(this).attr('class');

						$this.find("th[class*='sort-']").removeClass('sort-asc sort-desc');

						if (thisClass.search(/sort-asc/) > 0) {
							$(this).addClass('sort-desc');
						}
						else {
							$(this).addClass('sort-asc');
						}

						$this.ctLoad(opts, 'First');				
					});
				});
			}
			
			var tBodyElement = $this.find('.compassTable tbody');
		
			$('.compassTable tbody').livequery(function() {

				$this.ctIdTable();

				if (opts.striping) {
					$this.ctStripe(opts);
				}
				
				if (opts.resizable) {
					$this.ctResizable(opts);
				}

				if (opts.hover) {
					$this.ctHover(opts);
				}

				if (opts.selectable) {
					$this.ctSelectable(opts);
				}

				if (opts.checkboxToggle) {
					$this.ctCheckboxes(opts);
				}
				
				if (opts.toggle) {
					$this.ctToggleCols(opts);
				}
			});
			
			$().ajaxStart(function() {
				$('.ctPager div.load img').attr('src', opts.images+'/loading.gif');
			});
			
			$().ajaxStop(function() {
				$('.ctPager div.load img').attr('src', opts.images+'/load.png');
			});
			
			/* Make sure changes in the pager load table */
			if (opts.pager) {
				$('.ctPager').livequery(function() {
					$('.ctPager img').unbind('click').bind('click', function() {
						var src = $(this).attr('src');
						if (src.match(/disabled\.gif$/)) {
							return false;
						}
						var action = $(this).attr('alt');
						$this.ctLoad(opts, action);
					});

					$('.ctPager select').unbind('change').bind('change', function() {
						var parentClass = $(this).parent('div').attr('class').toString();

						if (parentClass == 'perPage') {
							/* When changing items per page, send user back to first page (current page may not exist) */
							$this.ctLoad(opts, 'First');
						}
						else {
							$this.ctLoad(opts, 'GoTo');
						}
					});
				});
			}
		});
	};
	
	$.fn.ctIdTable = function() {
		var $this = $(this);

		$this.find('thead th').each(function(i) {
			$(this).addClass('col-'+i);
			$(this).html('<div class="th-col-'+i+'">' + $(this).html() + '</div>');
			$this.find('tr').find('td:eq('+i+')').addClass('col-' + i);
			++i;
		});	
	};
	
	$.fn.ctStripe = function(opts) {
		var $this = $(this);
		
		$this.find('tbody tr:odd').addClass(opts.oddClass);
		$this.find('tbody tr:even').addClass(opts.evenClass);
	};
	
	$.fn.ctToggleCols = function(opts) {
		var current = $('.'+opts.toggleHolder).html();
		
		if (current != '') {
			return;
		}
		
		var $this = $(this);
		
		/* Create checkboxes for the columns */
		var xhtml = '';
		$this.find('th').each(function(i) {	
			xhtml += '<li class="col-' + i + '"><input type="checkbox" checked="checked" /> <span style="display: block;">' + $(this).text() + '</span><br /></li>';
			i++;
		});
		$('.'+opts.toggleHolder).html(xhtml);
		
		/* define starting width of <ul> */
		var maxWidth = 20;
		
		/* find widest <li> */
		$('.'+opts.toggleHolder+ ':first li').each(function() {
			
			$(this).css({float: 'none', width:'auto'});
			
			if ($(this).width() > maxWidth) {
				maxWidth = $(this).width();
			}
		});		
		
		/* Wrap checkboxes in valid <ul> */
		$('.'+opts.toggleHolder).html('<ul><li><span>Show/Hide Columns</span><ul>' + $('.'+opts.toggleHolder).html() + '</ul></li></ul>');
		
		/* Set width for <ul> now that we have valid <ul> */
		$('.'+opts.toggleHolder+' ul li ul').width(maxWidth + 40);
		
		/* Widen the clickable span */
		$('.'+opts.toggleHolder+' ul li ul li span').width(maxWidth + 40);
		
		/* Set Hover Class */
		$('.'+opts.toggleHolder).find('ul > li').hover(
			function() {
				$(this).toggleClass('hover');
			},
			function() {
				$(this).toggleClass('hover');
			}
		);
		
		/* add click functionality to toggle the columns */
		$('.'+opts.toggleHolder+' ul li ul li span').click(function() {		
			
			var thisClass = $(this).parent('li').attr('class').match(/col-[0-9]+/g);

			$this.find('.'+thisClass).toggle();
			var checked = $(this).parent().find('input:checkbox').attr('checked');
			$('.'+opts.toggleHolder+' ul li ul li.'+thisClass).find('input:checkbox').attr('checked', !checked);
		});
		
		$('.'+opts.toggleHolder+' ul li ul li input:checkbox').click(function() {
			var checked = $(this).attr('checked');
			var thisClass = $(this).parent().attr('class').match(/col-[0-9]+/g);
			$this.find('.'+thisClass).toggle();
			
			/* Adjust any other toggleHolder instances */
			$('.'+opts.toggleHolder+' ul li ul li.'+thisClass+ ' input:checkbox').attr('checked', checked);
		});
	};
	
	$.fn.ctHover = function(opts) {
		$this = $(this);
		
		$this.find('tbody tr').hover(
			function() {
				$(this).addClass(opts.hoverClass);
			},
			function() {
				$(this).removeClass(opts.hoverClass);
			}
		);
		
		$this.find('th').hover(
			function() {
				$(this).addClass(opts.hoverClass);
			},
			function() {
				$(this).removeClass(opts.hoverClass);
			}
		);
	};
	
	$.fn.ctSelectable = function(opts) {

		$this.find('td input:checkbox').click(function() {
			var checked = $(this).attr('checked');
			
			if (checked) {
				$(this).parent('td').parent('tr').addClass(opts.selectableClass);
			}
			else {
				$(this).parent('td').parent('tr').removeClass(opts.selectableClass);
			}
		});
		
		$this.find('td').click(function(e) {
			if($(this).hasClass('col-0')) {
				return;
			}
		
			$(this).parent('tr').toggleClass(opts.selectableClass);
		
			if (opts.checkboxToggle) {
				if($(this).parent('tr').hasClass(opts.selectableClass)) {
					$(this).parent('tr').find('td input:checkbox').attr('checked', true);
				}
				else {
					$(this).parent('tr').find('td input:checkbox').attr('checked', false);
				}
			}
		});
	};
	
	$.fn.ctResizable = function(opts) {
		$this = $(this);
		
		if ($this.find("th div").length == 0) {
			return false;
		}
	
		$this.find("th div").resizable({
			handles: 'e',
			helper: 'proxy',
			start: function(e, ui) {
				var $y = $(this).offset().top;
				var $height = $this.height();
				var thisClass = $(this).parent().attr('class').match(/col-[0-9]+/g);

				$this.css({opacity: opts.resizeOpacity});
				
				$('body').mousemove(function(event) {
					$(opts.resizer).css({
						'display': 'block',
				     	top: $y + 'px',
				     	left: (event.pageX + 0) + 'px',
						height: $height
					});
				});
			},
			stop: function(e, ui) {
				width = ui.size.width;
				thisClass = $(this).parent().attr('class').match(/col-[0-9]+/g);

				$this.find("th." + thisClass + ", th." + thisClass + " div:first").css({"width": width});
				$this.css({opacity:'1'});
				
				/* Add remaining width to next or previous cell */
				var totalCols = ui.element.parent("th").siblings().length;
				var deltax = 0;
				if (ui.size.width > ui.originalSize.width) {
					deltax = ui.size.width - ui.originalSize.width;
				} else {
					deltax = ui.originalSize.width - ui.size.width;
				}
				deltaxSplitter = deltax/totalCols;
				
				
				/* Convert from percentage to pixels */
				$this.find("th").each(function() {
					if ($(this).hasClass(thisClass)) {
					
					} else {
						if (ui.size.width > ui.originalSize.width) {
							var newWidth = $(this).width() - deltaxSplitter;
						} else {
							var newWidth = $(this).width() + deltaxSplitter;
						}
						$(this).css({ 'width': newWidth });
						$(this).find("div:first").css({ 'width': newWidth });
					}
				});

				$('body').unbind('mousemove');
				$(opts.resizer).css({display:'none'});
			}
		});
	};
	
	$.fn.ctCheckboxes = function(opts) {
		$this.find('th input:checkbox').click(function() {
			var checked = $(this).attr('checked');
			$this.find('td input:checkbox').attr('checked', checked);
			
			$this.find('td input:checkbox').parent('td').parent('tr').removeClass(opts.selectableClass);
			
			if (checked) {
				$this.find('td input:checkbox').parent('td').parent('tr').addClass(opts.selectableClass);
			}
		});
	};
	
	$.fn.ctAddPager = function(opts) {
		$this = $(this);
		
		var pager = 
			'<div class="ctPager">' +
			'<div class="inner"><div class="perPage">' +
			'Per Page: <select>';
			for (i=0; i<opts.perPageOptions.length; i++) {
				var selected = opts.perPage == opts.perPageOptions[i] ? ' selected="selected"' : '';
				pager += '<option value="'+opts.perPageOptions[i]+'"'+selected+'>'+opts.perPageOptions[i]+'</option>';
			}


		pager += '</select>' + '</div>' + 
			'<div class="separator"></div>' + 
			'<div class="pager">' + 
			'<div class="imgWrap" style="margin-right: 0px;"><img src="'+opts.images+'/first.gif" alt="First" title="First Page" /></div> ' + 
			'<div class="imgWrap"><img src="'+opts.images+'/prev.gif" alt="Previous" title="Previous" /></div> ' + 
			'<div class="separator"></div>' + 
			'<div style="float: left;" class="pages">Page <select>';
			
			for (var i=1; i<=$.fn.ctPagerData.pages; i++) {
				var selected = i == $.fn.ctPagerData.page ? ' selected="selected"' : '';
				pager += '<option value="'+i+'"'+selected+'>'+i+'</option>';
			}
			
		pager +=	
			'</select> of <span class="totalPages">' + $.fn.ctPagerData.pages + '</span></div>' + 
			'<div class="separator"></div>' + 
			'<div class="imgWrap" style="margin-right: 0px;"><img src="'+opts.images+'/next.gif" alt="Next" title="Next" /></div> ' + 
			'<div class="imgWrap"><img src="'+opts.images+'/last.gif" alt="Last" title="Last Page" /></div>' + 
			'</div>' + 
			'<div>' + 
			'<div class="separator"></div>' + 
			'<div class="imgWrap load"><img src="'+opts.images+'/loading.gif" alt="Load" title="Reload" /></div>' + 
			'<div class="separator"></div>' + 
			'<div class="displaying">Displaying ' + $.fn.ctPagerData.displayingStart + ' to ' + $.fn.ctPagerData.displayingEnd + ' of ' + $.fn.ctPagerData.found + ' items</div>' + 
			'</div>';
			
			if (opts.toggle) {
				pager = pager + '<div class="' + opts.toggleHolder + '"></div>';
			}
			
		pager += '</div>' + 
			'</div>';
		
		if (opts.pagerBefore) {
			$this.before(pager);
		}
		
		if (opts.pagerAfter) {
			$this.after(pager);
		}
		
		$('.ctPager .imgWrap').hover(
			function() {
				$(this).toggleClass('imgWrapHover');
			}, 
			function() {
				$(this).toggleClass('imgWrapHover');
			}
		);	
	};
	
	$.fn.ctLoadJson = function(opts) {
		$this = $(this);
		
		/* Set Variable to Build off of */
		var table = '';

		$.getJSON(opts.url,
			function(data) {		
				var cols = [];
				
				/* Loop through pager info */
				$.each(data.pager, function(i, v) {
				
					$.fn.ctPagerData = {
						page: 				v.page,
						pages: 				v.pages,
						displayingStart: 	v.displayingStart,
						displayingEnd: 		v.displayingEnd,
						found: 				v.found
					};
					
					if (v.found > 0) {
						if (opts.pager) {
							$this.ctAddPager(opts);
						}
						
						$this.ctSetPagerImages(opts, v.page, v.pages);
					}
					else {
						$this.remove();
					}
				});
				
				var table = '<thead><tr>';
				$.each(data.headings, function(i, heading) {
					var thisClass = heading.sort == '' ? '' : ' class="'+heading.sort+'"';
					var width = heading.width == 'undefined' ? '' : ' width="'+heading.width+'"';
					table += '<th'+thisClass+width+'>' + heading.display + '</th>';
					cols[i] = heading.id;
				});
				table += '</thead></tr>';
				
				$.fn.ctPagerData.cols = cols;
				
				table += '<tbody>';
				$.each(data.rows, function(i, row) {
					table += '<tr>';
					
					for (a=0; a < cols.length; a++) {
						table += '<td>' + row[cols[a]] + '</td>';
					}	
					
					table += '</tr>';
				});
				table += '</tbody>';

				$this.html(table);
				
				/* Make Updates */
				$('.ctPager div.load img').attr('src', opts.images+'/load.png');
			}
		);
	};
	
	$.fn.ctSetPagerImages = function(opts, page, pages) {
		$this = $(this);
		
		if (page == 1) {
			$this.parent().find(".ctPager img[src$='prev.gif']").attr('src', opts.images+'/prev-disabled.gif');
			$this.parent().find(".ctPager img[src$='first.gif']").attr('src', opts.images+'/first-disabled.gif');
			
			$this.parent().find('.ctPager').find("img[src$='next-disabled.gif']").attr('src', opts.images+'/next.gif');
			$this.parent().find('.ctPager').find("img[src$='last-disabled.gif']").attr('src', opts.images+'/last.gif');
		}
		
		if (page == pages) {
			$this.parent().find(".ctPager img[src$='next.gif']").attr('src', opts.images+'/next-disabled.gif');
			$this.parent().find(".ctPager img[src$='last.gif']").attr('src', opts.images+'/last-disabled.gif');
		
			if (page !== 1) {
				$this.parent().find(".ctPager img[src$='prev-disabled.gif']").attr('src', opts.images+'/prev.gif');
				$this.parent().find(".ctPager img[src$='first-disabled.gif']").attr('src', opts.images+'/first.gif');
			}
		}
		
		if (page !== 1 && page !== pages) {
			$this.parent().find('.ctPager').find("img[src$='next-disabled.gif']").attr('src', opts.images+'/next.gif');
			$this.parent().find('.ctPager').find("img[src$='last-disabled.gif']").attr('src', opts.images+'/last.gif');
			
			$this.parent().find(".ctPager img[src$='prev-disabled.gif']").attr('src', opts.images+'/prev.gif');
			$this.parent().find(".ctPager img[src$='first-disabled.gif']").attr('src', opts.images+'/first.gif');
		}
	};
	
	/* Main function to move through table. Will be binded to all previous/next/change functions */
	/* Add functionality to recognize filters and pass via ajax */
	$.fn.ctLoad = function(opts, action) {
	
		$this = $(this);
		
		/* Bind our ajax events now */
		$('.ctPager .displaying').html('Loading...');
		
		var widths= [];
		$this.find('tbody tr:eq(0) td').each(function(i) {
			widths[i] = $(this).width();
		});
		
		if (action !== 'undefined') {
			var moveToPage = parseInt($.fn.ctPagerData.page);
			
			switch (action) {
				case 'Next':
					moveToPage += 1;
					break;
					
				case 'Previous':
					moveToPage -= 1;
					break;
					
				case 'First':
					moveToPage = 1;
					break;
					
				case 'Last':
					moveToPage = $.fn.ctPagerData.pages;
					break;
					
				case 'GoTo':
					moveToPage = $('.ctPager .pages select').val();
					break;
			}
		}
		else {
			/* We are staying on the same page */
			var moveToPage = $.fn.ctPagerData.page;
		}
		
		/* Sort Info */
		var col = $this.find("th[class*='sort']").attr('class').toString();
		sortDir = col.match(/sort-(asc|desc)/g).toString();
		sortDir = sortDir.replace(/sort-/g, '');
		var sortCol = col.match(/col-[0-9]+/g).toString();
		sortCol = sortCol.replace(/col-/g, '');
		
		/* Results per Page */
		var perPage = $('.ctPager .perPage select').val();
		
		/* Create Get String */
		var get = '/' + opts.paramsStart + 'page' + opts.paramsJoin + moveToPage + opts.paramsSeparator + 'sortField' + opts.paramsJoin + $.fn.ctPagerData.cols[sortCol] + opts.paramsSeparator + 'sortOrder' + opts.paramsJoin + sortDir + opts.paramsSeparator + 'show' + opts.paramsJoin + perPage;
		
		var hidden = [];
		$('.'+opts.toggleHolder+':first ul li ul li input:checkbox').each(function(i) {
			var checked = $(this).attr('checked');
			if (checked === false) {
				hidden[i] = i;
			}
		});

		/* Grab Filter Values */
		var getFilters = '';
		$('#filterbox').find("input[type='text'], select").each(function() {
			var value = $(this).val();
			
			if (value != '' && value != 'undefined') {
				getFilters += $(this).attr('name') + '=' + value + '&';
			}
		});
		
		if (getFilters != '') {
			get += '?' + getFilters + 'foo=bar';
		}

		/* Set Variable to Build off of */
		var table = '';

		$.getJSON(opts.url+get,
			function(data) {		
				var cols = [];
				
				/* Loop through pager info */
				$.each(data.pager, function(i, v) {
					$.fn.ctPagerData = {
						page: 				v.page,
						pages: 				v.pages,
						displayingStart: 	v.displayingStart,
						displayingEnd: 		v.displayingEnd,
						found: 				v.found,
						cols:				$.fn.ctPagerData.cols		/* Make sure we don't lose our col ids */
					};
					
					$this.ctSetPagerImages(opts, v.page, v.pages);
					
					$('.ctPager .displaying').html('Displaying ' + $.fn.ctPagerData.displayingStart + ' to ' + $.fn.ctPagerData.displayingEnd + ' of ' + $.fn.ctPagerData.found + ' items');
					$('.ctPager .totalPages').html($.fn.ctPagerData.pages);
				});
				
				$.each(data.headings, function(i, heading) {
					cols[i] = heading.id;
				});
				
				var table = '';
				$.each(data.rows, function(i, row) {
					table += '<tr>';
					
					for (a=0; a < cols.length; a++) {
						var style = hidden[a] == a ? ' style="display: none;"' : '';
						table += '<td'+style+'>' + row[cols[a]] + '</td>';
					}	
					
					table += '</tr>';
				});
				
				$this.find('tbody').remove();
				$this.append('<tbody>'+table+'</tbody>');
				
				/* Update Pager Display */
				
				var options = '';
				for (var i=1; i<=$.fn.ctPagerData.pages; i++) {
					var selected = i == $.fn.ctPagerData.page ? ' selected="selected"' : '';
					options += '<option value="'+i+'"'+selected+'>'+i+'</option>';
				}
				$('.ctPager .pages select').html(options).val($.fn.ctPagerData.page);
			}
		);
	};
	
	/* Will be filled later by ajax call */
	$.fn.ctPagerData = {};
	
})(jQuery);