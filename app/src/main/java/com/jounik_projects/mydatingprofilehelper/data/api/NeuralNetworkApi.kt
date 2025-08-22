package com.jounik_projects.mydatingprofilehelper.data.api

/**
 * Interface for interacting with neural network APIs.
 * This interface defines the contract for any class that will provide
 * functionality related to neural network operations, specifically for
 * generating or assisting in the creation of profile descriptions.
 */
interface NeuralNetworkApi {

    /**
     * Calls a neural network to generate a profile description based on the provided input.
     * This is a suspend function, indicating that it performs a potentially long-running
     * operation and should be called from a coroutine.
     *
     * @param input The input data to the neural network (e.g., user interests, keywords).
     * @return A generated profile description as a String.
     */
    suspend fun generateProfileDescription(input: String): String
}