# Exchange Gain & Loss Calculator

## Overview

This document clarifies how the Exchange Gain & Loss Calculator functions and also serves as the specification for all of its functionality. The Calculator consists of the following Core functionality:

1. [Calculator Configuration](exchange-gain-and-loss-calculator.md#calculator-configuration)
2. [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results)
3. [Gain/Loss Consolidation](exchange-gain-and-loss-calculator.md#gain-loss-consolidation)

The application provides a single page that contains and allows users to interact with all functionality related to the Exchange Gain & Loss Calculator. This document explains how all components and functionality of this Calculator works.

![](../.gitbook/assets/image%20%286%29.png)

### Prerequisites

Please ensure you fulfill the following Prerequisites in order to ensure optimal and desired experience with this Calculator

1. You have Asset and Liability notes configured in [Financial Report Configuration](accounting-operation/financial-report-management-control-panel/)
2. You have at least 2 Asset, Liability, Income, Expense Accounts present in your [Chart of Account Configuration](accounting-operation/chart-of-account-management-panel.md)
3. You have a few transactions present within an [Active Accounting Period](accounting-operation/accounting-periods.md).

## Calculator Configuration Panel

This is a set of configurations that allows users to control Transactions and the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency) that the Calculator will use to calculate the Balances of all the accounts in its [Results](exchange-gain-and-loss-calculator.md#calculator-results).

All configurations are Persistent and will be saved for use across the entire application.

You can change the Calculator Configuration by clicking the **CONFIGURATION** button in the top-right corner of the Calculator.

![](../.gitbook/assets/image%20%2861%29.png)

This will present you with the actual Calculator Configuration Panel where you can change any of the Configurations described below.

![Prototype image of the Configurations panel for Exchange Gain/Loss Calculator](../.gitbook/assets/image%20%2812%29.png)

Once you are done making the desired changes to any of the configurations, press **SAVE** at the bottom of the form and the new configuration will be persisted.

### Consolidation Currency

This is the Currency in which all transactions are to be displayed within the calculator's results.  The balances of all accounts \(both for original transaction dates and on comparison date\) are displayed in this Currency. **The calculator will not be able to return any results if this currency is not configured**.

Once a value for this configuration is provided, it will persist until a user changes it again. If you navigate to another page and come back, this value will still be the same unless you or someone else changes it somehow.

### Account Configuration

The Calculator selects transactions from all Input level Asset and Liability accounts that are currently configured in the [Chart of Accounts](accounting-operation/chart-of-account-management-panel.md) to appear as [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results).

### Accounting Period

The Calculator selects transactions against [selected accounts](exchange-gain-and-loss-calculator.md#account-configuration) with voucher dates between the Start and End date \(inclusive\) of the selected [Accounting Period](accounting-operation/accounting-periods.md). 

All transactions that took place between the Start and End date of the selected [Accounting Period](accounting-operation/accounting-periods.md) must be factored in for the Calculator to calculate the balances for their respective Accounts \(in both the original date balance and comparison date balance\).

{% hint style="info" %}
By default, the [Active Accounting Period](accounting-operation/accounting-periods.md#active-accounting-period) is selected but you can change this by clicking **SET ACCOUNTING PERIOD** and submitting a new [Accounting Period](accounting-operation/accounting-periods.md) selection.
{% endhint %}

### Comparison Date

This is the date to which all transaction amounts are converted in order to identify the Balance of all the selected Accounts on this date. The Exchange Rates created for the same date as the Comparison date will be used to identify the value of all transactions in the [Consolidation Currency](exchange-gain-and-loss-calculator.md#consolidation-currency).

{% hint style="info" %}
By default this will be set to the value provided for the End Date of the [Active Accounting Period](accounting-operation/accounting-periods.md#active-accounting-period).
{% endhint %}

{% hint style="danger" %}
The Comparison Date must take place **at or after** the End Date specified in [Accounting Period](exchange-gain-and-loss-calculator.md#accounting-period).
{% endhint %}

### NET Gain/Loss Accounts

This is where you can configure the Accounts that must be transacted when consolidating Gain or Loss balances from any Result Account.

Consolidation of a **NET Gain** or **Loss** for any account will result in a vouchers being generated for the **Total NET Gain** and **Loss** amounts across all the selected accounts. Depending on whether the voucher is generated for the **Total NET Gain** or **Total NET Loss** there will be a single **Credit** or **Debit** transaction respectively and multiple transactions of other opposite type for all the accounts that were [Selected](exchange-gain-and-loss-calculator.md#selecting-calculator-results) for [Gain Loss Consolidation](exchange-gain-and-loss-calculator.md#gain-loss-consolidation).

This configuration allows you to:

1. Select the **Debit** account that will be transacted for **Total NET Gains** in the [Consolidation Voucher](exchange-gain-and-loss-calculator.md#consolidation-vouchers).
2. Select the **Credit** account that will be transacted for **Total NET Losses**  in the [Consolidation Voucher](exchange-gain-and-loss-calculator.md#consolidation-vouchers).

You can change these configurations by opening [Calculator Configuration Panel](exchange-gain-and-loss-calculator.md#calculator-configuration), updating the current values selected for these configurations and clicking **SAVE**.

## Transaction Metadata Filters

This allows you to configure a set of values for Transaction metadata that will be used as filters to select Transactions to be factored into the balance calculations in the Calculator Result Accounts. Currently you can apply filters on:

1. Offices
2. Journals
3. Projects

These filter configurations only take affect once at least one value has been provided for that field. Otherwise, the calculator will not filter transactions that metadata field. For example, if no Office is selected then transactions from any Office may be selected for the Calculator. However, if you select at least one office then only transactions from that specific office will be selected by the Calculator.

{% hint style="info" %}
These are optional and you do not need to set values for this in order for the Calculator to return results.
{% endhint %}

### Applying Transaction Filters

Press the **TRANSACTION FILTERS** button in the [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results) toolbar.

![Prototype image of the Calculator Results toolbar.](../.gitbook/assets/image%20%2846%29.png)

This will show you the form for the **Voucher Transaction Filters** where you can provide values for the desired filterable metadata

![Prototype image of the Transaction Filters form.](../.gitbook/assets/image%20%283%29.png)

Once you have selected all the desired values for the metadata filter fields, please click **APPLY** and you will be returned to the Calculator page where you will now see updated balance values for the result Accounts. These balances are now calculated using transactions that only satisfy the metadata field filter values specified in your filter form.

{% hint style="info" %}
You can select multiple values for each metadata filter field in the form.
{% endhint %}

Clicking **CANCEL** in this form will simply return you back to the Calculator Results without applying any filters.

{% hint style="info" %}
Once you have filters applied, you can clear them by pressing the **CLEAR TRANSACTION FILTERS** button available in the Calculator Results toolbar.
{% endhint %}

## Calculator Results

The Calculator runs calculations for all the Accounts selected in [Account Configuration](exchange-gain-and-loss-calculator.md#account-configuration) and returns a Result for each account. Each Result displays the account Code, Name and:

1. The Account's [Balance on Original Date](exchange-gain-and-loss-calculator.md#balance-on-original-date)
2. The Account's [Balance on Comparison Date](exchange-gain-and-loss-calculator.md#balance-on-comparison-date)
3. The GAIN or LOSS amount for the account based on the [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation)

![](../.gitbook/assets/image%20%2848%29.png)

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

If Balance of the Account on [Original Date](exchange-gain-and-loss-calculator.md#balance-on-original-date) is greater than on Comparison Date, then we have an Exchange GAIN. This must be displayed as a GAIN amount.

If Balance of the Account on Comparison Date is greater than on Original date, then we have an Exchange LOSS. This must be displayed as a LOSS amount.

### Selecting Calculator Results

Every account in [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results) allows you to select it by clicking the empty checkbox next to it. Selected accounts have their checkbox filled out.

## Gain Loss Consolidation

When the Calculator is done fetching Results, you can consolidate Gain and Loss balances from select Result Accounts or a group of Result Accounts altogether.

If the Balance Calculation is not complete yet for a certain account, you can still choose to flag it for Consolidation as soon as the calculation is complete for that account. However, you will not be able to view the [Voucher Transaction Preview](exchange-gain-and-loss-calculator.md#voucher-transaction-preview) for those accounts that have not completed their [Balance Calculation](exchange-gain-and-loss-calculator.md#balance-calculation).

You can perform Gain/Loss Consolidation for [Selected](exchange-gain-and-loss-calculator.md#selecting-calculator-results) result accounts by pressing the **CONSOLIDATE EXCHANGE GAIN/LOSS** button available in the [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results) toolbar **once you have selected at least one result**.

You will be directed to the Consolidation page where you can view [Voucher Transaction Previews](exchange-gain-and-loss-calculator.md#voucher-transaction-preview) for both the [Gain and Loss Consolidation Vouchers](exchange-gain-and-loss-calculator.md#consolidation-vouchers).

![Prototype image of the Gain/Loss Consolidation panel](../.gitbook/assets/image%20%2819%29.png)

Please take a moment to understand what [Consolidation Vouchers](exchange-gain-and-loss-calculator.md#consolidation-vouchers) and [Voucher Transaction Previews](exchange-gain-and-loss-calculator.md#voucher-transaction-preview) are before proceeding to [Commit ](exchange-gain-and-loss-calculator.md#committing-consolidation)the Consolidation Vouchers.

### Consolidation Vouchers

A distinct voucher is created for the **Total NET Gains** and **Total NET Losses** when you choose to Consolidate the selected accounts. The **Consolidation Vouchers** are have a unique transaction for each Account in your current selection of Calculator Results. The Consolidation Voucher transactions are slightly different for **Total NET Gains** and **Losses.**

{% hint style="info" %}
These are commonly referred to as C/L vouchers in the organization.
{% endhint %}

{% hint style="warning" %}
A Consolidation Voucher for any account \(that appears in [Calculator Results](exchange-gain-and-loss-calculator.md#calculator-results)\) can only be created once per [Accounting Period](accounting-operation/accounting-periods.md).
{% endhint %}

**NET Gain Voucher**

This voucher holds one **Credit** transaction for the **NET Gain Account** selected in [NET Gain/Loss Accounts](exchange-gain-and-loss-calculator.md#net-gain-loss-accounts) and one **Debit** transaction for every selected [Calculator Result](exchange-gain-and-loss-calculator.md#selecting-calculator-results) that had resulted in an Exchange Gain.

#### NET Loss Voucher

This voucher holds one **Debit** transaction for the **NET Loss Account** selected in [NET Gain/Loss Accounts](exchange-gain-and-loss-calculator.md#net-gain-loss-accounts) and one **Credit** transaction for every selected [Calculator Result](exchange-gain-and-loss-calculator.md#selecting-calculator-results) that had resulted in an Exchange Loss.

### Voucher Transaction Preview

This shows you a preview version of the transactions that will be generated as a result of Gain Loss Consolidation for the selected accounts. It is useful for visualizing the vouchers for NET Gains and Losses before actually committing them.

You can only view this preview once you have selected at least one [Calculator Result](exchange-gain-and-loss-calculator.md#calculator-results).

The preview also allows you to set the following details for the vouchers that will be generated:

1. Voucher Description
2. Voucher Journal
3. Voucher Type
4. Voucher Office

For the purposes of keeping data integrity, Voucher Date and Voucher Currency will be locked to whatever is specified in the [Calculator Configuration](exchange-gain-and-loss-calculator.md#calculator-configuration-panel)

![Voucher details for a C/L voucher](../.gitbook/assets/image%20%2811%29.png)

### Committing Consolidation

Once you are happy with your voucher transaction previews, you can click the **COMMIT** button in the Gain Loss Consolidation panel Toolbar.

![](../.gitbook/assets/image%20%2823%29.png)

Once you do this, the application will generate the vouchers that you had previewed and also mark the accounts that were selected for Consolidation as **CONSOLIDATED.** This is so that users cannot create another Consolidation for accounts that have already been consolidated.

