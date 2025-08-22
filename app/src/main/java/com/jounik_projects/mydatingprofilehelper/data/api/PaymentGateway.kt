package com.jounik_projects.mydatingprofilehelper.data.api

/**
 * Interface representing a payment gateway.
 * This interface is a placeholder for integrating with actual payment systems
 * like Telegram Bot API or other payment gateways.
 */
interface PaymentGateway {

    /**
     * Processes a payment with the specified amount and description.
     *
     * @param amount The amount of the payment.
     * @param description A description of the payment.
     * @return true if the payment processing was successful, false otherwise.
     */
    fun processPayment(amount: Int, description: String): Boolean
}