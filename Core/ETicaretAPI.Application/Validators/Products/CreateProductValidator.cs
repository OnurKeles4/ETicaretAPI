using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entites;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator() {  
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Lütfen ürün adını boş bırakmayınız.")
                .MaximumLength(150).WithMessage("Lütfen ürün adını uygun uzunlukta seçiniz (0-150)");

         RuleFor(p => p.Stock).NotEmpty().NotNull().WithMessage("Lütfen stok adını boş bırakmayınız.")
                .Must(s => s > 0).WithMessage("Lütfen stoğu pozitif bir sayı yazınız");

            RuleFor(p => p.Price).NotEmpty().NotNull().WithMessage("Lütfen fiyat bilgisini boş bırakmayınız.")
       .Must(s => s > 0).WithMessage("Lütfen fiyatı pozitif bir sayı yazınız");



        }


    }
}
