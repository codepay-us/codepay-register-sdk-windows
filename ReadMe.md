## Connection and Transaction Logic

This document explains how the POS/ECR client and CodePay payment app server manage their connection and handle transactions. It covers different situations that can affect the connection and how to ensure transactions are sent properly.

### Overview

The POS/ECR client and CodePay payment app server keep a long-lasting connection. For each transaction, the client first checks if it is connected to the server. If it is connected, it sends the transaction. However, there are situations where the connection may be lost, and the client needs to handle these cases carefully.

### Connection Scenarios

1. **Normal Operation**
   - **Description**: When the client is connected to the server, transactions are sent directly. If the connection status is "connected," the transaction is sent immediately.

2. **Controlled Disconnection by Server**
   - **When This Happens**: The server either:
     - Exits ECR mode, or
     - Shuts down or closes the app normally.
   - **Client Behavior**: The server tells the client it has disconnected, so the client marks the connection as “disconnected.” Now, if the client wants to send a transaction, it will first reconnect to the server and then send the transaction.

3. **Unexpected Disconnection (e.g., Network Loss or Device Shutdown)**
   - **When This Happens**: The network is lost, or the terminal is turned off suddenly.
   - **Client Behavior**: The server can’t notify the client in this case, so the client still thinks it is connected. If the client tries to send a transaction, it will get an error showing the connection is actually broken. Then, the client will:
     - Mark the connection as “disconnected.”
     - Try to reconnect.
     - After reconnecting, resend the transaction.

### Demo Code Logic

In the demo code, there is a two-step transaction process:

- **First Transaction Attempt**: Checks if the connection is really active. If it is not, it will try to reconnect.
- **Second Transaction Attempt**: After confirming the connection, it sends the actual transaction.

### Summary

This logic ensures transactions are sent correctly by checking and fixing the connection status when needed. The two-step approach handles both normal and unexpected disconnections, helping the client keep working smoothly even if network issues happen. 

===================================================================================================================================

## Post-Integration Testing

After the integration is complete, you can perform the following tests to ensure the system works as expected:

### Steps for Testing

1. **Initiate Transaction from POS/ECR**
   - Begin a transaction on the POS/ECR side, which will trigger the payment process.

2. **Disconnect Network on POS/ECR**
   - Once the payment terminal receives the payment request, disconnect the network connection on the POS/ECR side. This simulates a network loss or disconnection.

3. **Complete Transaction on Payment Terminal**
   - Proceed to complete the transaction on the payment terminal. Allow the payment terminal to finish the transaction successfully.

4. **Reconnect POS/ECR Network**
   - Wait for 2-4 seconds after the transaction is completed on the terminal.
   - Reconnect the network on the POS/ECR side.

5. **POS/ECR Automatic Reconnection**
   - The POS/ECR system should automatically reconnect to the server once the network is restored.
   - The POS/ECR should receive the transaction data returned by the server, confirming the successful transaction.

### Expected Outcome

- The POS/ECR system should be able to handle the temporary disconnection and automatically reconnect to the server.
- After the network is restored, the POS/ECR should receive the correct transaction data from the server without requiring manual intervention.

This test ensures the system is resilient to network interruptions and can recover and complete transactions correctly once the network is restored.