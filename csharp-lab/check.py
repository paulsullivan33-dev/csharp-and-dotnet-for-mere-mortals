#!/usr/bin/env python3
"""Independent cross-check for the StockReport program (t03).

Reads the same fixture with python's csv module and works the sums
out on its own. Nothing here uses C#: every figure must match.
Usage: python3 check.py stock.csv
"""
import csv
import sys
from collections import defaultdict
from decimal import Decimal

rows = list(csv.DictReader(open(sys.argv[1])))
items = [
    (r["Sku"], r["Name"], r["Category"], int(r["Quantity"]), Decimal(r["UnitPrice"]))
    for r in rows
]

print("lines:", len(items))
print("total units:", sum(i[3] for i in items))
print("total value:", sum(i[3] * i[4] for i in items))
print("average unit price:",
      (sum(i[4] for i in items) / len(items)).quantize(Decimal("0.01")))

by_cat = defaultdict(Decimal)
for i in items:
    by_cat[i[2]] += i[3] * i[4]
for cat, value in sorted(by_cat.items(), key=lambda kv: -kv[1]):
    print("category", cat, value)

top = max(items, key=lambda i: i[3] * i[4])
print("most valuable:", top[0], top[3] * top[4])
print("low stock (<30):",
      [(i[0], i[3]) for i in sorted((i for i in items if i[3] < 30),
                                     key=lambda i: i[3])])
