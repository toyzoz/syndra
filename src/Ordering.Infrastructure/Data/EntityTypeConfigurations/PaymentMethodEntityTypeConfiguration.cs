using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.AggregateModels.Buyers;

namespace Ordering.Infrastructure.Data.EntityTypeConfigurations;

public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {

    }
}