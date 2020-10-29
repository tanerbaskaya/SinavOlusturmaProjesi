﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.Models
{
    public class Wired
    {
		private int id;
		private string title;
		private string description;

		public string Title { get => title; set => title = value; }
		public string Description { get => description; set => description = value; }
		public int Id { get => id; set => id = value; }
	}
}
