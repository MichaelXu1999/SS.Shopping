﻿using System;
using SiteServer.Plugin;
using SS.Payment.Core;

namespace SS.Shopping.Core.Parse
{
    public static class StlShoppingPay
    {
        public const string ElementName = "stl:shoppingPay";

        public const string AttributeSuccessUrl = "successUrl";
        public const string AttributeWeixinName = "weixinName";

        //public static object ApiPayGet(IRequest context)
        //{
        //    var siteId = context.GetPostInt("siteId");
        //    var sessionId = context.GetPostString("sessionId");

        //    if (context.IsUserLoggin)
        //    {
        //        CartRepository.UpdateUserName(siteId, sessionId, context.UserName);
        //    }

        //    var addressInfoList = AddressDao.GetAddressInfoList(context.UserName, sessionId);
        //    var cartInfoList = CartRepository.GetCartInfoList(siteId, context.UserName, sessionId);

        //    AddressInfo addressInfo = null;
        //    foreach (var addInfo in addressInfoList)
        //    {
        //        if (addInfo.IsDefault) addressInfo = addInfo;
        //    }
        //    if (addressInfo == null && addressInfoList.Count > 0)
        //    {
        //        addressInfo = addressInfoList[0];
        //    }

        //    var deliveryInfoList = DeliveryRepository.GetDeliveryInfoList(siteId);
        //    DeliveryInfo deliveryInfo = null;
        //    if (deliveryInfoList.Count > 0)
        //    {
        //        deliveryInfo = deliveryInfoList[0];
        //    }

        //    var totalCount = 0;
        //    decimal totalFee = 0;
        //    var deliveryFee = Utils.GetDeliveryFee(cartInfoList, addressInfo, deliveryInfo);

        //    foreach (var cartInfo in cartInfoList)
        //    {
        //        totalCount += cartInfo.Count;
        //        totalFee += cartInfo.Fee * cartInfo.Count;
        //    }

        //    return new
        //    {
        //        addressInfoList,
        //        addressId = addressInfo?.Id ?? 0,
        //        deliveryInfoList,
        //        deliveryId = deliveryInfo?.Id ?? 0,
        //        cartInfoList,
        //        totalCount,
        //        totalFee,
        //        deliveryFee
        //    };
        //}

        //public static object ApiPaySaveAddress(IRequest context)
        //{
        //    var siteId = context.GetPostInt("siteId");
        //    var sessionId = context.GetPostString("sessionId");
        //    var deliveryId = context.GetPostInt("deliveryId");
        //    var addressInfo = context.GetPostObject<AddressInfo>("addressInfo");
        //    var isEdit = context.GetPostBool("isEdit");

        //    addressInfo.UserName = context.UserName;
        //    addressInfo.SessionId = sessionId;

        //    if (isEdit)
        //    {
        //        AddressDao.Update(addressInfo);
        //    }
        //    else
        //    {
        //        addressInfo.Id = AddressDao.Insert(addressInfo);
        //    }

        //    AddressDao.SetDefault(context.UserName, sessionId, addressInfo.Id);

        //    var cartInfoList = CartRepository.GetCartInfoList(siteId, context.UserName, sessionId);
        //    var deliveryInfo = DeliveryRepository.GetDeliveryInfo(deliveryId);
        //    var deliveryFee = Utils.GetDeliveryFee(cartInfoList, addressInfo, deliveryInfo);

        //    return new
        //    {
        //        addressInfo,
        //        deliveryFee
        //    };
        //}

        //public static object ApiPayRemoveAddress(IRequest context)
        //{
        //    var addressId = context.GetPostInt("addressId");

        //    AddressDao.Delete(addressId);

        //    return new {};
        //}

        //public static object ApiPaySetAddressAndDelivery(IRequest context)
        //{
        //    var siteId = context.GetPostInt("siteId");
        //    var sessionId = context.GetPostString("sessionId");
        //    var addressId = context.GetPostInt("addressId");
        //    var deliveryId = context.GetPostInt("deliveryId");

        //    AddressDao.SetDefault(context.UserName, sessionId, addressId);

        //    var cartInfoList = CartRepository.GetCartInfoList(siteId, context.UserName, sessionId);
        //    var addressInfo = AddressDao.GetAddressInfo(addressId);
        //    var deliveryInfo = DeliveryRepository.GetDeliveryInfo(deliveryId);

        //    var deliveryFee = Utils.GetDeliveryFee(cartInfoList, addressInfo, deliveryInfo);

        //    return new
        //    {
        //        deliveryFee
        //    };
        //}

        //public static object ApiPay(IRequest context)
        //{
        //    var siteId = context.GetPostInt("siteId");
        //    var sessionId = context.GetPostString("sessionId");
        //    var addressId = context.GetPostInt("addressId");
        //    var channel = context.GetPostString("channel");
        //    var totalFee = context.GetPostDecimal("totalFee");
        //    var deliveryFee = context.GetPostDecimal("deliveryFee");
        //    var totalCount = context.GetPostInt("totalCount");
        //    var message = context.GetPostString("message");
        //    var cartIdList = context.GetPostObject<List<string>>("cartIdList").Select(cartId => Utils.ParseInt(cartId)).ToList();
        //    var isMobile = context.GetPostBool("isMobile");
        //    var successUrl = context.GetPostString("successUrl");
        //    if (string.IsNullOrEmpty(successUrl))
        //    {
        //        successUrl = Context.SiteApi.GetSiteUrl(siteId);
        //    }

        //    var guid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        //    var paymentApi = new PaymentApi(siteId);

        //    var siteInfo = Context.SiteApi.GetSiteInfo(siteId);

        //    var addressInfo = AddressDao.GetAddressInfo(addressId);
        //    var orderInfo = new OrderInfo
        //    {
        //        SiteId = siteId,
        //        Guid = guid,
        //        AddDate = DateTime.Now,
        //        Address = addressInfo.Address,
        //        ZipCode = addressInfo.ZipCode,
        //        Location = addressInfo.Location,
        //        Message = message,
        //        Mobile = addressInfo.Mobile,
        //        Channel = channel,
        //        TotalFee = totalFee,
        //        ExpressCost = deliveryFee,
        //        RealName = addressInfo.RealName,
        //        UserName = context.UserName,
        //        SessionId = sessionId,
        //        IsPayed = false,
        //        State = string.Empty,
        //        Tel = addressInfo.Tel,
        //        TotalCount = totalCount
        //    };

        //    orderInfo.Id = OrderRepository.Insert(orderInfo);

        //    var cartInfoList = CartRepository.GetCartInfoList(siteId, context.UserName, sessionId);
        //    var newCardIdList = new List<int>();
        //    foreach (var newCardInfo in cartInfoList)
        //    {
        //        newCardIdList.Add(newCardInfo.Id);
        //    }

        //    //CartDao.UpdateOrderId(cartIdList, orderInfo.Id);
        //    CartRepository.UpdateOrderId(newCardIdList, orderInfo.Id);

        //    var amount = totalFee + deliveryFee;
        //    var orderNo = guid;
        //    successUrl = $"{successUrl}?guid={guid}";
        //    if (channel == "alipay")
        //    {
        //        return isMobile
        //            ? paymentApi.ChargeByAlipayMobi(siteInfo.SiteName, amount, orderNo, successUrl)
        //            : paymentApi.ChargeByAlipayPc(siteInfo.SiteName, amount, orderNo, successUrl);
        //    }
        //    if (channel == "weixin")
        //    {
        //        var apiUrl = Context.PluginApi.GetPluginApiUrl(Main.PluginId);

        //        var notifyUrl = $"{apiUrl}/{nameof(ApiPayWeixinNotify)}/{orderNo}";
        //        var url = HttpUtility.UrlEncode(paymentApi.ChargeByWeixin(siteInfo.SiteName, amount, orderNo, notifyUrl));
        //        var qrCodeUrl =
        //            $"{apiUrl}/{nameof(ApiPayQrCode)}?qrcode={url}";
        //        return new
        //        {
        //            qrCodeUrl,
        //            orderNo
        //        };
        //    }
        //    if (channel == "jdpay")
        //    {
        //        return paymentApi.ChargeByJdpay(siteInfo.SiteName, amount, orderNo, successUrl);
        //    }

        //    return new
        //    {
        //        guid,
        //        amount
        //    };
        //}

        //public static HttpResponseMessage ApiPayQrCode(IRequest context)
        //{
        //    var response = new HttpResponseMessage();

        //    var qrcode = context.GetQueryString("qrcode");
        //    var qrCodeEncoder = new QRCodeEncoder
        //    {
        //        QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
        //        QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M,
        //        QRCodeVersion = 0,
        //        QRCodeScale = 4
        //    };

        //    //将字符串生成二维码图片
        //    var image = qrCodeEncoder.Encode(qrcode, Encoding.Default);

        //    //保存为PNG到内存流  
        //    var ms = new MemoryStream();
        //    image.Save(ms, ImageFormat.Png);

        //    response.Content = new ByteArrayContent(ms.GetBuffer());
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        //    response.StatusCode = HttpStatusCode.OK;

        //    return response;
        //}

        //public static HttpResponseMessage ApiPayWeixinNotify(IRequest context, string orderNo)
        //{
        //    var response = new HttpResponseMessage();

        //    var paymentApi = new PaymentApi(context.GetQueryInt("siteId"));

        //    bool isPayed;
        //    string responseXml;
        //    paymentApi.NotifyByWeixin(HttpContext.Current.Request, out isPayed, out responseXml);
        //    if (isPayed)
        //    {
        //        OrderRepository.UpdateIsPayed(orderNo);
        //    }

        //    response.Content = new StringContent(responseXml);
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
        //    response.StatusCode = HttpStatusCode.OK;

        //    return response;
        //}

        //public static object ApiPayWeixinInterval(IRequest context)
        //{
        //    var orderNo = context.GetPostString("orderNo");

        //    var isPayed = OrderRepository.IsPayed(orderNo);

        //    return new
        //    {
        //        isPayed
        //    };
        //}

        public static string Parse(IParseContext context)
        {
            var successUrl = string.Empty;
            var weixinName = string.Empty;

            foreach (var attriName in context.StlAttributes.AllKeys)
            {
                var value = context.StlAttributes[attriName];
                if (Utils.EqualsIgnoreCase(attriName, AttributeSuccessUrl))
                {
                    successUrl = Context.ParseApi.ParseAttributeValue(value, context);
                }
                else if (Utils.EqualsIgnoreCase(attriName, AttributeWeixinName))
                {
                    weixinName = Context.ParseApi.ParseAttributeValue(value, context);
                }
            }

            var elementId = "el-" + Guid.NewGuid();
            var vueId = "v" + Guid.NewGuid().ToString().Replace("-", string.Empty);

            var pluginUrl = Context.PluginApi.GetPluginUrl(Main.PluginId);
            var apiUrl = Context.Environment.ApiUrl;

            var jqueryUrl = $"{pluginUrl}/assets/js/jquery.min.js";
            var utilsUrl = $"{pluginUrl}/assets/js/utils.js";
            var vueUrl = $"{pluginUrl}/assets/js/vue.min.js";
            var deviceUrl = $"{pluginUrl}/assets/js/device.min.js";
            var baseCssUrl = $"{pluginUrl}/assets/css/base.css";
            var locationCssUrl = $"{pluginUrl}/assets/css/location.css";
            var locationJsUrl = $"{pluginUrl}/assets/js/location.js";
            var apiGetUrl = $"{apiUrl}/{Main.PluginId}/ApiPayGet";
            var apiSaveAddressUrl = $"{apiUrl}/{Main.PluginId}/ApiPaySaveAddress";
            var apiRemoveAddressUrl = $"{apiUrl}/{Main.PluginId}/ApiPayRemoveAddress";
            var apiSetAddressAndDelivery = $"{apiUrl}/{Main.PluginId}/ApiPaySetAddressAndDelivery";
            var apiPayUrl = $"{apiUrl}/{Main.PluginId}/ApiPay";
            var apiWeixinIntervalUrl = $"{apiUrl}/{Main.PluginId}/ApiPayWeixinInterval";

            var paymentApi = new PaymentApi(context.SiteId);

            string template;
            if(!string.IsNullOrEmpty(context.StlInnerHtml))
            {
                template = Context.ParseApi.Parse(context.StlInnerHtml, context);
            }
            else
            {
                template = @"
<div class=""w1060"">
      <div class=""orderinfo g_orderinfo"">
        <div class=""g_order_mid"">
          <h2>收货信息</h2>
          <div class=""g_order_more"">
            <span class=""g_o_more"" @click=""isAddressList = true"">更多收货信息>></span>
            <span class=""g_o_add"" @click=""addAddress()""><b>新增收货地址</b><i>+</i></span>
          </div>
          <div class=""clear""></div>
          <ul class=""g_order_ul get_order"">
            <li v-for=""addressInfo in addressInfoList"" :class=""{ cut: addressInfo.id == addressId }"" @click=""setAddress(addressInfo)"">
              <div class=""g_o_title"">
                <span class=""g_o_name"">{{ addressInfo.realName }}</span>
                <span class=""g_o_tel"">{{ addressInfo.mobile || addressInfo.tel }}</span>
              </div>
              <div class=""g_o_address"">
                {{ addressInfo.location + ' ' + addressInfo.address }}
              </div>
            </li>
          </ul>
          <div class=""g_o_way"" v-show=""deliveryInfoList && deliveryInfoList.length > 1"">
            运送方式：
            <select v-model=""deliveryId"" @change=""setAddressAndDelivery"">
                <option v-for=""deliveryInfo in deliveryInfoList"" v-bind:value=""deliveryInfo.id"">{{ deliveryInfo.deliveryName }}</option>
            </select>
          </div>
        </div>
        <div class=""order_h2 g_order"">
          商品清单
        </div>
        <div class=""orderinfo_div"">
          <ul class=""cart_ul"">
            <li v-for=""cartInfo in cartInfoList"">
              <a :href=""cartInfo.linkUrl"" class=""img_h2"" target=""_blank"">
                <img :src=""cartInfo.imageUrl"" />
                <h2>{{ cartInfo.productName }}</h2>
              </a>
              <span class=""cart_price"">¥{{ cartInfo.fee.toFixed(2) }}</span>
              <div class=""cart_number"">
                <div class=""cart_num_text"">x<span>{{ cartInfo.count }}</span></div>
              </div>
            </li>
          </ul>
          <div class=""orderinfo_pay"">
            <span>共{{ totalCount }}个商品</span>
            <span>商品金额：¥{{ totalFee.toFixed(2) }}元</span>
            <span>运费：¥{{ deliveryFee.toFixed(2) }}元</span>
            <span class=""g_o_color"">合计：<b>¥{{ (totalFee + deliveryFee).toFixed(2) }}元</b></span>
            <div class=""clear""></div>
          </div>
        </div>

        <div class=""pay_list g_o_pay"">
          <p>支付方式</p>
          <ul>
            <li v-show=""(isAlipayPc && !isMobile) || (isAlipayMobi && isMobile)"" :class=""{ pay_cut: channel === 'alipay' }"" @click=""channel = 'alipay'"" class=""channel_alipay""><b></b></li>
            <li v-show=""isWeixin"" :class=""{ pay_cut: channel === 'weixin' }"" @click=""channel = 'weixin'"" class=""channel_weixin""><b></b></li>
            <li v-show=""isJdpay"" :class=""{ pay_cut: channel === 'jdpay' }"" @click=""channel = 'jdpay'"" class=""channel_jdpay""><b></b></li>
          </ul>
        </div>
        <div class=""order_other"">
          <h2>其他</h2>
          <div class=""g_o_text"">
            <textarea placeholder=""如需其他问题请留言"" v-model=""message""></textarea>
          </div>
        </div>
        <div class=""get_order_btn"">
          <div class=""get_o_pay"">应付金额：<b>¥{{ (totalFee + deliveryFee).toFixed(2) }}元</b></div>
          <a href=""javascript:;"" @click=""pay"" class=""g_o_btn"" :class=""{ 'g_o_btn_disabled': !this.addressId || !this.channel || !this.totalFee || !this.totalCount }"">去结算</a>
        </div>

      </div>
    </div>
    <div class=""address_mask"" style=""display: none"" v-show=""isAddressAdd || isAddressList"" @click=""isAddressAdd = isAddressList = false""></div>
    <div class=""address_lists"" v-show=""isAddressList"">
      <div class=""address_title"">收货地址</div>
      <div class=""address_close add_close1"" @click=""isAddressList = false""></div>
      <ul class=""g_order_ul address_ul"">
        <li v-for=""addressInfo in addressInfoList"" :class=""{ cut: addressInfo.id == addressId }"" @click=""setAddress(addressInfo)"">
          <div class=""g_o_title"">
            <span class=""g_o_name"">{{ addressInfo.realName }}</span>
            <span class=""g_o_tel"">{{ addressInfo.mobile || addressInfo.tel }}</span>
          </div>
          <div class=""g_o_address"">
            {{ addressInfo.location + ' ' + addressInfo.address }}
          </div>
          <div class=""add_del"" @click=""removeAddress(addressInfo)"">删除</div>
          <div class=""eidt"" @click=""editAddress(addressInfo)"">编辑</div>
        </li>
      </ul>
    </div>
    <div class=""add_cont"" v-show=""isAddressAdd"">
      <div class=""address_title"">收货地址</div>
      <div class=""address_close add_close"" @click=""isAddressAdd = false""></div>
      <ul class=""add_ul"">
        <li><span>收货人</span><input type=""text"" v-model=""addressInfo.realName"" /></li>
        <li class=""fleft""><span>手机号码</span><input type=""text"" v-model=""addressInfo.mobile"" /></li>
        <li class=""fright""><span>固定电话</span><input type=""text"" v-model=""addressInfo.tel"" /></li>
        <li><span>所在地区</span>
          <ul id=""list1"">
            <li id=""summary-stock"">
              <div class=""dd"">
                <div id=""store-selector"">
                  <div class=""text"">
                    <div>{{ addressInfo.location }}</div><b></b>
                  </div>
                  <div onclick=""$('#store-selector').removeClass('hover');event.stopPropagation();"" class=""close""></div>
                </div>
                <div id=""store-prompt""><strong></strong></div>
              </div>
            </li>
          </ul>
        </li>
        <li class=""add_ul_input""><span>详细地址</span><input type=""text"" v-model=""addressInfo.address"" /></li>
        <li><span>邮编</span><input type=""text"" v-model=""addressInfo.zipCode"" /></li>
      </ul>
      <a href=""javascript:;"" @click=""saveAddress(addressInfo)"" class=""add_save"">保存并提交</a>
    </div>
";
                if (!string.IsNullOrEmpty(weixinName))
                {
                    weixinName = $@"<p style=""text-align: center"">{weixinName}</p>";
                }
                template += $@"
<div class=""mask1_bg mask1_bg_cut"" v-show=""isWxQrCode"" @click=""isWxQrCode = false""></div>
<div class=""detail_alert detail_alert_cut"" v-show=""isWxQrCode"">
  <div class=""close"" @click=""isWxQrCode = false""></div>
  <div class=""pay_list"">
    <p style=""text-align: center"">打开手机微信，扫一扫下面的二维码，即可完成支付</p>
    {weixinName}
    <p style=""margin-left: 195px;margin-bottom: 80px;""><img :src=""qrCodeUrl"" style=""width: 200px;height: 200px;""></p>
  </div>
</div>
";
            }

            return $@"
<script type=""text/javascript"" src=""{jqueryUrl}""></script>
<script type=""text/javascript"" src=""{utilsUrl}""></script>
<script type=""text/javascript"" src=""{vueUrl}""></script>
<script type=""text/javascript"" src=""{deviceUrl}""></script>
<link rel=""stylesheet"" type=""text/css"" href=""{baseCssUrl}"" />
<link rel=""stylesheet"" type=""text/css"" href=""{locationCssUrl}"" />
<div id=""{elementId}"">
    {template}
</div>
<script type=""text/javascript"">
    var sessionId = shoppingGetSessionId();
    var {vueId} = new Vue({{
        el: '#{elementId}',
        data: {{
            addressInfoList: [],
            addressId: 0,
            deliveryInfoList: [],
            deliveryId: 0,
            cartInfoList: [],
            totalCount: 0,
            totalFee: 0,
            deliveryFee: 0,
            isAddressAdd: false,
            isAddressList: false,
            addressInfo: {{}},
            message: '',
            channel: 'alipay',
            isAlipay: {paymentApi.IsAliPay.ToString().ToLower()},
            isWeixin: {paymentApi.IsWxPay.ToString().ToLower()},
            isMobile: device.mobile(),
            isWxQrCode: false,
            qrCodeUrl: ''
        }},
        methods: {{
            saveAddress: function (addressInfo) {{
                var $this = this;
                var isEdit = addressInfo.id && addressInfo.id > 0
                addressInfo.location = $(""#store-selector .text div"").html();
                this.isAddressAdd = false;
                $.ajax({{
                    url : ""{apiSaveAddressUrl}"",
                    xhrFields: {{
                        withCredentials: true
                    }},
                    type: ""POST"",
                    data: JSON.stringify({{
                        siteId: {context.SiteId},
                        sessionId: sessionId,
                        deliveryId: $this.deliveryId,
                        addressInfo: addressInfo,
                        isEdit: isEdit
                    }}),
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function(data)
                    {{
                        $this.deliveryFee = data.deliveryFee;
                        $this.addressId = data.addressInfo.id;
                        if (!isEdit) {{
                            $this.addressInfoList.push(data.addressInfo);
                            return;
                        }}
                        for(var i = 0; i < $this.addressInfoList.length; i++) {{
                            var a = $this.addressInfoList[i];
                            if (a.id === data.id) {{
                                a = data;
                            }}
                        }}
                    }},
                    error: function (err)
                    {{
                        var err = JSON.parse(err.responseText);
                        console.log(err.message);
                    }}
                }});
            }},
            setAddress: function (addressInfo) {{
                this.addressId = addressInfo.id;
                this.isAddressList = false;
                this.setAddressAndDelivery();
            }},
            addAddress: function () {{
                this.addressInfo = {{}};
                this.isAddressAdd = true;
            }},
            editAddress: function (addressInfo) {{
                this.addressInfo = addressInfo;
                this.isAddressAdd = true;
            }},
            removeAddress: function (addressInfo) {{
                var index = this.addressInfoList.indexOf(addressInfo);
                if (index > -1) {{
                    this.addressInfoList.splice(index, 1);
                    $.ajax({{
                        url : ""{apiRemoveAddressUrl}"",
                        xhrFields: {{
                            withCredentials: true
                        }},
                        type: ""POST"",
                        data: JSON.stringify({{
                            siteId: '{context.SiteId}',
                            addressId: addressInfo.id
                        }}),
                        contentType: ""application/json; charset=utf-8"",
                        dataType: ""json"",
                        success: function(data)
                        {{
                            console.log('removed');
                        }},
                        error: function (err)
                        {{
                            var err = JSON.parse(err.responseText);
                            console.log(err.message);
                        }}
                    }});
                }}
            }},
            setAddressAndDelivery: function () {{
                var $this = this;
                $.ajax({{
                    url : ""{apiSetAddressAndDelivery}"",
                    xhrFields: {{
                        withCredentials: true
                    }},
                    type: ""POST"",
                    data: JSON.stringify({{
                        siteId: '{context.SiteId}',
                        sessionId: sessionId,
                        addressId: this.addressId,
                        deliveryId: this.deliveryId
                    }}),
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function(data)
                    {{
                        $this.deliveryFee = data.deliveryFee;
                    }},
                    error: function (err)
                    {{
                        var err = JSON.parse(err.responseText);
                        console.log(err.message);
                    }}
                }});
            }},
            weixinInterval: function(orderNo) {{
                var $this = this;
                var interval = setInterval(function(){{
                    $.ajax({{
                        url : ""{apiWeixinIntervalUrl}"",
                        xhrFields: {{
                            withCredentials: true
                        }},
                        type: ""POST"",
                        data: JSON.stringify({{orderNo: orderNo}}),
                        contentType: ""application/json; charset=utf-8"",
                        dataType: ""json"",
                        success: function(data)
                        {{
                            if (data.isPayed) {{
                                location.href = '{successUrl}?guid=' + orderNo;
                            }}
                        }},
                        error: function (err)
                        {{
                            var err = JSON.parse(err.responseText);
                            console.log(err.message);
                        }}
                    }});
                }}, 3000);
            }},
            pay: function () {{
                var cartIdList = [];
                for(var i = 0; i < this.cartInfoList.length; i++) {{
                    cartIdList.push(this.cartInfoList[i].id);
                }}
                if (cartIdList.length === 0 || !this.addressId || !this.channel || !this.totalFee || !this.totalCount) return;
                var $this = this;

                $.ajax({{
                    url : ""{apiPayUrl}"",
                    xhrFields: {{
                        withCredentials: true
                    }},
                    type: ""POST"",
                    data: JSON.stringify({{
                        siteId: {context.SiteId},
                        sessionId: sessionId,
                        addressId: this.addressId,
                        channel: this.channel,
                        totalFee: this.totalFee,
                        deliveryFee: this.deliveryFee,
                        totalCount: this.totalCount,
                        message: this.message,
                        isMobile: this.isMobile,
                        cartIdList: cartIdList,
                        successUrl: '{successUrl}'
                    }}),
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function(charge)
                    {{
                        if ($this.channel === 'weixin') {{
                            $this.isWxQrCode = true;
                            $this.qrCodeUrl = charge.qrCodeUrl;
                            $this.weixinInterval(charge.orderNo);
                        }} else {{
                            document.write(charge);
                        }}
                    }},
                    error: function (err)
                    {{
                        var err = JSON.parse(err.responseText);
                        console.log(err.message);
                    }}
                }});
            }},
        }}
    }});
    $(document).ready(function(){{
        $.ajax({{
            url : ""{apiGetUrl}"",
            xhrFields: {{
                withCredentials: true
            }},
            type: ""POST"",
            data: JSON.stringify({{
                siteId: '{context.SiteId}',
                sessionId: sessionId
            }}),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function(data)
            {{
                {vueId}.addressInfoList = data.addressInfoList;
                {vueId}.addressId = data.addressId;
                {vueId}.deliveryInfoList = data.deliveryInfoList;
                {vueId}.deliveryId = data.deliveryId;
                {vueId}.cartInfoList = data.cartInfoList;
                {vueId}.totalCount = data.totalCount;
                {vueId}.totalFee = data.totalFee;
                {vueId}.deliveryFee = data.deliveryFee;
            }},
            error: function (err)
            {{
                var err = JSON.parse(err.responseText);
                console.log(err.message);
            }}
        }});
    }});
</script>
<script type=""text/javascript"" charset=""utf-8"" src=""{locationJsUrl}""></script>
";
        }
    }
}
