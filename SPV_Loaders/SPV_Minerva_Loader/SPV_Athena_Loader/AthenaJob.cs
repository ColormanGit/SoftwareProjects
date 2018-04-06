using System;

public class AthenaJob
{
    public AthenaJob() { }

    public AthenaJob(string orderID, Object[] inputFromFile)
    {
        this.isDecimal = false;
        initiateAutoInputArray(orderID, inputFromFile);
    }

    [System.Xml.Serialization.XmlIgnore] public string[] autoInputArray { get; set; }

    // AthenaJob Properties
    public bool isDecimal { get; set; }

    // Auto input fields
    [System.Xml.Serialization.XmlIgnore] public string orderID { get; set; }
    public string pecasJobNumber { get; set; }
    public string dueDate { get; set; }
    public string purchaseOrderNumber { get; set; }
    public string purchaseOrderLine { get; set; }
    public string pecasOrderNumber { get; set; }
    public string customerAccountCode { get; set; }
    public string buildQuantity { get; set; }
    public string ascmOrderID { get; set; }
    public string endCustomer { get; set; }
    public string activationSystem { get; set; }
    public string productType { get; set; }
    public string erpMaterialCode { get; set; }
    public string integratorPartID { get; set; }
    public string integratorID { get; set; }
    public string activationType { get; set; }
    public string partNumber { get; set; }
    public string retailBarcode { get; set; }
    public string retailBarcodeType { get; set; }

    // Manual input fields
    public string client { get; set; }
    public string contractType { get; set; }
    public string artworkPartNumber { get; set; }
    public string jobQty { get; set; }
    public string packQty { get; set; }
    public string boxQty { get; set; }
    public string palletQty { get; set; }
    public string jobType { get; set; }
    public string activationTypeID { get; set; }
    public string denomination { get; set; }
    public string denominationCurrency { get; set; }
    public string faiStart { get; set; }
    public string faiEnd { get; set; }
    public string intelJobType { get; set; }
    public string country { get; set; }
    public string alternativePartNumber { get; set; }
    public string packagingGTIN { get; set; }
    public string incommProductDescription { get; set; }

    public string bomFileName { get; set; }
    public string bomComment1 { get; set; }
    public string bomComment2 { get; set; }
    public string bomComment3 { get; set; }
    public string bomComment4 { get; set; }
    public string bomComment5 { get; set; }
    public string pkpn { get; set; }



    // Create and initiate and array for auto input
    private void initiateAutoInputArray(string orderIDNumber, Object[] inputFromFile)
    {
        autoInputArray = new string[19];

        orderID = orderIDNumber;
        pecasJobNumber = inputFromFile[0].ToString();
        dueDate = inputFromFile[1].ToString();
        purchaseOrderNumber = inputFromFile[2].ToString();
        purchaseOrderLine = inputFromFile[3].ToString();
        pecasOrderNumber = inputFromFile[4].ToString();
        customerAccountCode = inputFromFile[5].ToString();
        buildQuantity = inputFromFile[6].ToString();
        ascmOrderID = inputFromFile[7].ToString();
        endCustomer = inputFromFile[8].ToString();
        activationSystem = inputFromFile[9].ToString();
        productType = inputFromFile[10].ToString();
        erpMaterialCode = inputFromFile[11].ToString();
        integratorPartID = inputFromFile[12].ToString();
        integratorID = inputFromFile[13].ToString();
        activationType = inputFromFile[14].ToString();
        partNumber = inputFromFile[15].ToString();
        retailBarcode = inputFromFile[16].ToString();
        retailBarcodeType = inputFromFile[17].ToString();

        autoInputArray[0] = orderID;
        autoInputArray[1] = pecasJobNumber;
        autoInputArray[2] = dueDate;
        autoInputArray[3] = purchaseOrderNumber;
        autoInputArray[4] = purchaseOrderLine;
        autoInputArray[5] = pecasOrderNumber;
        autoInputArray[6] = customerAccountCode;
        autoInputArray[7] = buildQuantity;
        autoInputArray[8] = ascmOrderID;
        autoInputArray[9] = endCustomer;
        autoInputArray[10] = activationSystem;
        autoInputArray[11] = productType;
        autoInputArray[12] = erpMaterialCode;
        autoInputArray[13] = integratorPartID;
        autoInputArray[14] = integratorID;
        autoInputArray[15] = activationType;
        autoInputArray[16] = partNumber;
        autoInputArray[17] = retailBarcode;
        autoInputArray[18] = retailBarcodeType;
    }

    // Overriden ToString Method
    public override string ToString()
    {
        return orderID + " " 
            + pecasJobNumber + " " 
            + dueDate + " "
            + purchaseOrderNumber + " " 
            + purchaseOrderLine + " " 
            + pecasOrderNumber + " " 
            + customerAccountCode + " "
            + buildQuantity + " " 
            + ascmOrderID + " "
            + endCustomer + " "
            + activationSystem + " "
            + productType + " " 
            + erpMaterialCode + " "
            + integratorPartID + " "
            + integratorID + " "
            + activationType + " "
            + partNumber + " " 
            + retailBarcode + " " 
            + retailBarcodeType;
    }
}