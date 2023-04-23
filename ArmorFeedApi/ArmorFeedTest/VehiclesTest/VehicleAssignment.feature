Feature: VehicleAllocationTest
As a developer
	I want to assign a vehicle to an Enterprise
	So that I will be able to trace the current state of my vehicles

Scenario: Assign a vehicle to a shipment
	Given the Endpoint https://localhost:5017/api/v1/sign-up is available
	And A Enterprise is already stored in Enterpise's Data
	And A Customer is already stored in Customer's Data
	And A Shipment is already stored in Shipment's Data
	When A Post Request is sent
	Then A Response with Status 200 is Received