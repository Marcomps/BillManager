﻿namespace FacturaManager.Domain.Interfaces;

public interface ITenantEntity
{
    Guid TenantId { get; set; }
}