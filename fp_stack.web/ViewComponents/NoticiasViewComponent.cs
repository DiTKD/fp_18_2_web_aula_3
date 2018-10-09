using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fp_stack.core.Services;

namespace fp_stack.web.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {
        private NotciaService _notciaService;

        public NoticiasViewComponent( core.Services.NotciaService notciaService)
        {
            _notciaService = notciaService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int total, bool noticiasUrgentes=false)
        {
            var view = noticiasUrgentes ? "noticiasUrgentes" : "noticias";
            var itens = _notciaService.GetItens(total);

            return View(view, itens);
        }

       
    }
    
   
}
