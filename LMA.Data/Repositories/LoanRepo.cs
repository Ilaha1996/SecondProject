﻿using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.DAL;

namespace LMA.Data.Repositories;
public class LoanRepo : GenericRepo<Loan>, ILoanRepo { }