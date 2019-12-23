# API & Data Structure

## Employees

{% api-method method="get" host="https://devtes.cha-net.org" path="/v1/employees" %}
{% api-method-summary %}
Employees
{% endapi-method-summary %}

{% api-method-description %}
This endpoint allows you to get the details of a select employee.
{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}
{% api-method-headers %}
{% api-method-parameter name="Authentication" type="string" required=true %}
Authentication token to track down who is emptying our stocks.
{% endapi-method-parameter %}
{% endapi-method-headers %}
{% endapi-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}
Cake successfully retrieved.
{% endapi-method-response-example-description %}

```
{    "name": "Cake's name",    "recipe": "Cake's recipe name",    "cake": "Binary cake"}
```
{% endapi-method-response-example %}

{% api-method-response-example httpCode=401 %}
{% api-method-response-example-description %}
You get this when you don't have permissions to read or perform actions on the resource\(s\). 
{% endapi-method-response-example-description %}

```

```
{% endapi-method-response-example %}

{% api-method-response-example httpCode=404 %}
{% api-method-response-example-description %}
Could not find a cake matching this query.
{% endapi-method-response-example-description %}

```
{    "message": "Ain't no cake like that."}
```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}

{% api-method method="get" host="https://devtes.cha-net.org" path="/v1/employees/:id" %}
{% api-method-summary %}
Employee
{% endapi-method-summary %}

{% api-method-description %}
This endpoint allows you to get the details of a select employee.
{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}
{% api-method-path-parameters %}
{% api-method-parameter name="id" type="string" %}
ID of the cake to get, for free of course.
{% endapi-method-parameter %}
{% endapi-method-path-parameters %}

{% api-method-headers %}
{% api-method-parameter name="Authentication" type="string" required=true %}
Authentication token to track down who is emptying our stocks.
{% endapi-method-parameter %}
{% endapi-method-headers %}

{% api-method-query-parameters %}
{% api-method-parameter name="page\_start" type="integer" required=false %}
The position in the list of total employees to start the list from.
{% endapi-method-parameter %}

{% api-method-parameter name="page\_size" type="integer" required=false %}
Default is 50.
{% endapi-method-parameter %}

{% api-method-parameter name="full\_name" type="string" required=false %}
The name or last name of the employee.
{% endapi-method-parameter %}

{% api-method-parameter name="id" type="string" %}
The API will do its best to find a cake matching the provided recipe.
{% endapi-method-parameter %}

{% api-method-parameter name="code" type="boolean" %}
Whether the cake should be gluten-free or not.
{% endapi-method-parameter %}
{% endapi-method-query-parameters %}
{% endapi-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}
Cake successfully retrieved.
{% endapi-method-response-example-description %}

```
{    "name": "Cake's name",    "recipe": "Cake's recipe name",    "cake": "Binary cake"}
```
{% endapi-method-response-example %}

{% api-method-response-example httpCode=404 %}
{% api-method-response-example-description %}
Could not find a cake matching this query.
{% endapi-method-response-example-description %}

```
{    "message": "Ain't no cake like that."}
```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}

{% api-method method="post" host="" path="" %}
{% api-method-summary %}
Add Employee
{% endapi-method-summary %}

{% api-method-description %}

{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}
{% api-method-path-parameters %}
{% api-method-parameter name="" type="string" required=false %}

{% endapi-method-parameter %}
{% endapi-method-path-parameters %}
{% endapi-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}

{% endapi-method-response-example-description %}

```

```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}

{% api-method method="get" host="" path="/api/v1/countries" %}
{% api-method-summary %}
Get Countries
{% endapi-method-summary %}

{% api-method-description %}

{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}

{% endapi-method-response-example-description %}

```javascript
[
    {
        id: "234234324",
        name: "name value",
    },
    {
        id: "234234324",
        name: "name value",
    },
    {
        id: "234234324",
        name: "name value",
    },
    {
        id: "234234324",
        name: "name value",
    },
    {
        id: "234234324",
        name: "name value",
    },
    {
        id: "234234324",
        name: "name value",
    }
]
```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}

{% api-method method="get" host="" path="/api/v1/provinces" %}
{% api-method-summary %}
Get Provinces
{% endapi-method-summary %}

{% api-method-description %}

{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}
{% api-method-path-parameters %}
{% api-method-parameter name="countryId" type="string" required=false %}
When this is provided, only fetches provinces whose countryId matches this countryId
{% endapi-method-parameter %}
{% endapi-method-path-parameters %}
{% endapi-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}

{% endapi-method-response-example-description %}

```javascript
[
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
    {
        id: "32423432",
        countryId: "324324324",
        name: "province name value",
    },
]
```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}

