# Exchange Gain & Loss Calculator

## Overview

This document clarifies how the Exchange Gain & Loss Calculator functions and also serves as the specification for all of its functionality. The Calculator consists of the following Core functionality:

1. [Calculator Configuration](exchange-gain-and-loss-calculator.md#calculator-configuration)
2. [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results)
3. [Gain/Loss Consolidation](exchange-gain-and-loss-calculator.md#gain-loss-consolidation)

### Prerequisites

Please ensure you fulfill the following Prerequisites in order to ensure optimal and desired experience with this Calculator

1. You have Asset and Liability notes configured in [Financial Report Configuration](accounting-operation/financial-report-management-control-panel/)
2. You have at least 2 Asset, Liability, Income, Expense Accounts present in your [Chart of Account Configuration](accounting-operation/chart-of-account-management-panel.md)
3. You have a few transactions present within an [Active Accounting Period](accounting-operation/accounting-periods.md).

## Calculator Configuration Panel

This is a set of configurations that allows users to control Transactions and the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency) that the Calculator will use to calculate the Balances of all the accounts in its [Results](exchange-gain-and-loss-calculator.md#calculator-results).

Except for [Transaction Metadata Filters](exchange-gain-and-loss-calculator.md#transaction-metadata-filters) all other configurations are Persistent and will be saved for use across the entire application.

### Consolidation Currency

This is the Currency in which all transactions are to be displayed within the calculator's results.  The balances of all accounts \(both for original transaction dates and on comparison date\) are displayed in this Currency. The calculator will not be able to return any results if this currency is not configured.

Once a value for this configuration is provided, it will persist until a user changes it again. If you navigate to another page and come back, this value will still be the same unless you or someone else changes it somehow.

### Account Configuration

When fetching Transactions for the [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation), the Calculator will only fetch Transactions from Accounts that are configured here. If no Accounts are selected here, the Calculator will simply not return any results.

{% hint style="warning" %}
By default, only Input level Asset and Liability accounts will be selected here. If you would like to provide a different Account configuration, please use Custom Account Configuration.

Changing the Account Configuration can be a heavy process because the application needs to **generate background metadata for new Account selections and delete the background metadata for deselected Accounts** in order to keep load times for the [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation) optimal.
{% endhint %}

### Accounting Period

The Calculator selects transactions against [selected accounts](exchange-gain-and-loss-calculator.md#account-configuration) with voucher dates between the Start and End date \(inclusive\) of the selected [Accounting Period](accounting-operation/accounting-periods.md). 

All transactions that took place between the Start and End date of the selected [Accounting Period](accounting-operation/accounting-periods.md) must be factored in for the Calculator to calculate the balances for their respective Accounts \(in both the original date balance and comparison date balance\).

{% hint style="info" %}
By default, the [Active Accounting Period](accounting-operation/accounting-periods.md#active-accounting-period) is selected but you can change this by clicking **SET ACCOUNTING PERIOD** and submitting a new [Accounting Period](accounting-operation/accounting-periods.md) selection.
{% endhint %}

### Comparison Date

This is the date to which all transaction amounts are converted in order to identify the Balance of all the selected Accounts on this date. The Exchange Rates created for the same date as the Comparison date will be used to identify the value of all transactions in the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency).

{% hint style="info" %}
By default this will be set to the value provided for the End Date of the current Financial Year.
{% endhint %}

{% hint style="danger" %}
The Comparison Date must take place **at or after** the End Date specified in [Accounting Period](exchange-gain-and-loss-calculator.md#accounting-period).
{% endhint %}

### NET Gain/Loss Accounts

This is where you can configure the Accounts that must be transacted when consolidating Gain or Loss balances from any Result Account.

Consolidation of a **NET Gain** or **Loss** for any account will result in a vouchers being generated for the **Total NET Gain** and **Loss** amounts across all the selected accounts. Depending on whether the voucher is generated for the **Total NET Gain** or **Total NET Loss** there will be a single **Credit** or **Debit** transaction respectively and multiple transactions of other opposite type for all the accounts that were selected for [Gain Loss Consolidation](exchange-gain-and-loss-calculator.md#gain-loss-consolidation).

This configuration allows you to select the **Credit** account that will be transacted for **Total NET Gains** and the **Debit** account that will be transacted for **Total NET Losses.** You do this by clicking on the **SET GAIN/LOSS ACCOUNTS** button in the [Calculator Configuration Panel](exchange-gain-and-loss-calculator.md#calculator-configuration).

### Transaction Metadata Filters

This allows you to configure a set of values for Transaction metadata that will be used as filters to select Transactions to be factored into the balance calculations in the Calculator Result Accounts. Currently you can apply filters on:

1. Offices
2. Journals
3. Projects

These filter configurations only take affect once at least one value has been provided for that field. Otherwise, the calculator will not filter transactions that metadata field. For example, if no Office is selected then transactions from any Office may be selected for the Calculator. However, if you select at least one office then only transactions from that specific office will be selected by the Calculator.

## Calculator Results

The Calculator runs calculations for all the Accounts selected in [Account Configuration](exchange-gain-and-loss-calculator.md#account-configuration) and returns a Result for each account. Each Result displays the account Code, Name and:

1. The Account's [Balance on Original Date](exchange-gain-and-loss-calculator.md#balance-on-original-date)
2. The Account's [Balance on Comparison Date](exchange-gain-and-loss-calculator.md#balance-on-comparison-date)
3. The GAIN or LOSS amount for the account based on the [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation)

### Balance on Original Date

This is the Balance of the Account when using Consolidated \(in the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency)\) Transaction amount values on the transactions' original dates.

### Balance on Comparison Date

This is the Balance of the Account when using Consolidated \(in the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency)\) Transaction amount values on specified [Comparison Date](exchange-gain-and-loss-calculator.md#comparison-date) in the Calculator Configuration.

### Balance Calculation

This calculation sums up the total Debits and Credits \(converted to the Consolidation Currency\) across all the Transactions that are factored in through the [Calculator Configuratio](exchange-gain-and-loss-calculator.md#calculator-configuration)n. This allows it to identify the balances of all Accounts.

The calculator holds two Balance records for all the factored Accounts:

1. Balance of the Account when transaction credits and debits are converted to the Consolidation Currency using Exchange Rates provided for their original Voucher date as described [here](exchange-gain-and-loss-calculator.md#balance-on-original-date).
2. Balance of the Account when transaction credits and debits are converted to the Consolidation Currency using Exchange Rates provided for the Comparison Date as described [here](exchange-gain-and-loss-calculator.md#balance-on-comparison-date).

These two records are used to calculate the resulting Exchange Gain or Loss Amount for that specific account. This is displayed as a GAIN or LOSS amount in the Calculator Results listing.

### Selecting Calculator Results

Every account in [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results) allows you to select it by clicking the empty checkbox next to it. Selected accounts have their checkbox filled out.

## Gain Loss Consolidation

When the Calculator is done fetching Results, you can consolidate Gain and Loss balances from select Result Accounts or a group of Result Accounts altogether.

If the Balance Calculation is not complete yet for a certain account, you can still choose to flag it for Consolidation as soon as the calculation is complete for that account. However, you will not be able to view the [Voucher Transaction Preview](exchange-gain-and-loss-calculator.md#voucher-transaction-preview) for those accounts that have not completed their [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation).

### Consolidation Vouchers

A distinct voucher is created for the **Total NET Gains** and **Total NET Losses** when you choose to Consolidate the selected accounts. The **Consolidation Vouchers** are have a unique transaction for each Account in your current selection of Calculator Results. The Consolidation Voucher transactions are slightly different for **Total NET Gains** and **Losses.**

{% hint style="warning" %}
A Consolidation Voucher for any account \(that appears in [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results)\) can only appear once per [Accounting Period](accounting-operation/accounting-periods.md).
{% endhint %}

**NET Gain Voucher**

This voucher holds one **Debit** transaction for the **NET Gain Account** selected in [NET Gain/Loss Accounts](exchange-gain-and-loss-calculator.md#net-gain-loss-accounts) and one **Credit** transaction for every selected [Calculator Result](exchange-gain-and-loss-calculator.md#selecting-calculator-results) that had resulted in an Exchange Gain.

#### NET Loss Voucher

This voucher holds one **Credit** transaction for the **NET Loss Account** selected in [NET Gain/Loss Accounts](exchange-gain-and-loss-calculator.md#net-gain-loss-accounts) and one **Debit** transaction for every selected [Calculator Result](exchange-gain-and-loss-calculator.md#selecting-calculator-results) that had resulted in an Exchange Loss.

### Voucher Transaction Preview

This shows you a preview version of the transactions that will be generated as a result of Gain Loss Consolidation for the selected accounts. It is useful for visualizing the vouchers for NET Gains and Losses before actually committing them.

You can only view this preview once you have selected at least one [Calculator Result](exchange-gain-and-loss-calculator.md#calculator-results).

