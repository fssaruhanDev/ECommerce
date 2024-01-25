using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Commands.ShoppingCart;

public class GetBuyShoppingCartCommandHandle : IRequestHandler<GetBuyShoppingCartCommand, Guid>
{

    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetBuyShoppingCartCommandHandle(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(GetBuyShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await shoppingCartRepository.GetByIdAsync(request.shoppingCartId);
        /// Satın alma kontrolleri yapılıp satın alma işleminin yapıldığından emin olduktan sonra.

        #region Önemli Not
        /*
         * Normalde Bu kısımda kullanıcının database'deki shoppingCart satırını silmemiz gerek çünkü order tablosu ile çok işlem olmuyor fakat
         * sepet işlemleri her anlamda çok fazla işlem tutan ve hareketli olan kısımlardır. Database şişmesi vb durumlardan kaçabilmek için
         * E Ticaret sitelerinde bu alanı sürekli temizlerler ve bende sistemimi buna göre kurmak istedim fakat configuration ayarlarında göremediğim 
         * yada fark edemediğim bir hatadan dolayı  ".OnDelete(DeleteBehavior.Restrict);" ayarım çalışmıyor bu nedenle bu alan şu an ne yazık ki istenilen 
         * isteklere cevap veremiyor. Şu an için is active kısmını false yapıp etrafını doşıyorum fakat bu ayarlamaların da getirdiği bazı bozukluklar mevcut 
         * buda kişinin yeni bir ShoppingCart açmasını engelliyor. Kısaca ne yazık ki bu alanda size cevap veremiyorum şu anlık.
         */
        //await shoppingCartRepository.DeleteAsync(shoppingCart);
        #endregion

        shoppingCart.IsActive = false;

        await shoppingCartRepository.AddAsync(shoppingCart);

        return shoppingCart.ID;
    }
}
