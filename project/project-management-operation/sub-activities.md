# Sub Activities

## Sub Activities Panel

This is where you can manage all sub-activities for a Project Activity. Every activity created in the Activity Control Panel can have multiple sub-activities.

The Activity's progress, is calculated by getting the average progress across all of its sub-activities.

### Toolbar

Sub activities toolbar allows you to:

1. [Add Sub Activity](sub-activities.md#adding-a-sub-activity)

## Adding a Sub Activity

You can add a sub activity by clicking the **ADD** button in the [Sub Activities Toolbar](sub-activities.md#toolbar) and filling out the subsequent form.

{% hint style="info" %}
This table specifies all the form fields. Mandatory fields end with a "\*" symbol.
{% endhint %}

| Field | Data Type | Input Method | Notes |
| :--- | :--- | :--- | :--- |
| Name | Text | Manual text input |  |
| Description | Text\(long\) | Manual text input |  |
| Recurring | Boolean | On/Off switch |  |
| Planned Start Date | Date | Calendar date selection |  |
| Planned Due Date | Date | Calendar date selection |  |
| Assignee | Employee | Employee search control |  |
| Documents | List of documents | Local file selections |  |
| Target | decimal number | Manual numeric input |  |
| Target Unit | Unit Type | Unit Type Dropdown |  |

## Extend Target

## Extend Due Date

When you want to extend the Final Due Date for any activity \(or sub-activity\), you must create an [Extension](extensions.md#add-an-extension) for it. You can extend the Due Date for a Sub Activity by clicking the **EXTEND ACTIVITY** button in its Actions list and submitting the subsequent form.

{% hint style="info" %}
The following table specifies all the fields for the form. Mandatory fields end with "\*" symbol.
{% endhint %}

| Field | Data Type | Input Method | Notes |
| :--- | :--- | :--- | :--- |
| Start Date \* | Date | Calendar date selection |  |
| End Date \* | Date | Calendar date selection |  |
| Description | Text\(long\) | Manual text input |  |

{% hint style="warning" %}
You can only add an Extension if its duration timeline does not coincide with another Extension for the same Activity.
{% endhint %}

The extension will now be available in the [Extensions](extensions.md) for the same Activity.

## Start Activity

Starting an Activity will set the Actual Start Date of the Sub Activity to the date specified by the user. 

{% hint style="warning" %}
Once you submit this, you cannot change it. It is **NON REVERSIBLE**
{% endhint %}

## End Activity

## Editing Sub Activity

## Add Document

## Delete Document

## Delete Activity

