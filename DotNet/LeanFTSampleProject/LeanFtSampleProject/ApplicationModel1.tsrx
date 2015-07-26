<?xml version="1.0" encoding="utf-8"?>
<applicationModel>
  <name>Magento Commerce App Model</name>
  <className>MagentoCommerce</className>
  <objects>
    <object technology="Web" type="Page">
      <name>Madison Island</name>
      <codeName>MadisonIslandPage</codeName>
      <identification>
        <properties />
      </identification>
      <children>
        <object technology="Web" type="EditField">
          <name>q</name>
          <codeName>QEditField</codeName>
          <identification>
            <properties>
              <property name="Type" type="string">search</property>
              <property name="TagName" type="string">INPUT</property>
              <property name="Name" type="string">q</property>
            </properties>
          </identification>
          <children />
        </object>
        <object technology="Web" type="Button">
          <name>Search</name>
          <codeName>SearchButton</codeName>
          <identification>
            <properties>
              <property name="ButtonType" type="string">submit</property>
              <property name="TagName" type="string">BUTTON</property>
              <property name="Name" type="string">Search</property>
            </properties>
          </identification>
          <children />
        </object>
      </children>
    </object>
  </objects>
</applicationModel>