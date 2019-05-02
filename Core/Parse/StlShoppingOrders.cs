﻿using System;
using System.Text;
using SiteServer.Plugin;
using SS.Payment.Core;

namespace SS.Shopping.Core.Parse
{
    public static class StlShoppingOrders
    {
        public const string ElementName = "stl:shoppingOrders";

        public const string AttributeSuccessUrl = "successUrl";
        public const string AttributeOrderUrl = "orderUrl";
        public const string AttributeWeixinName = "weixinName";

        //public static object ApiOrdersGet(IRequest context)
        //{
        //    var type = context.GetPostString("type");

        //    var orderInfoList = new List<OrderInfo>();
        //    if (context.IsUserLoggin)
        //    {
        //        orderInfoList = string.IsNullOrEmpty(type)
        //            ? OrderRepository.GetOrderInfoList(context.UserName, string.Empty)
        //            : OrderRepository.GetOrderInfoList(context.UserName, Convert.ToBoolean(type));

        //        foreach (var orderInfo in orderInfoList)
        //        {
        //            orderInfo.CartInfoList = CartRepository.GetCartInfoList(orderInfo.Id);
        //        }
        //    }

        //    return new
        //    {
        //        context.IsUserLoggin,
        //        orderInfoList
        //    };
        //}

        //public static object ApiOrdersRemove(IRequest context)
        //{
        //    if (context.IsUserLoggin)
        //    {
        //        var orderId = context.GetPostInt("orderId");
        //        OrderRepository.Delete(orderId);
        //    }

        //    return new { };
        //}

        //public static object ApiOrdersPay(IRequest context)
        //{
        //    if (!context.IsUserLoggin) return null;

        //    var siteId = context.GetPostInt("siteId");
        //    var orderId = context.GetPostInt("orderId");
        //    var channel = context.GetPostString("channel");
        //    var isMobile = context.GetPostBool("isMobile");
        //    var successUrl = context.GetPostString("successUrl");
        //    if (string.IsNullOrEmpty(successUrl))
        //    {
        //        successUrl = Context.SiteApi.GetSiteUrl(siteId);
        //    }

        //    var siteInfo = Context.SiteApi.GetSiteInfo(siteId);
        //    var orderInfo = OrderRepository.GetOrderInfo(orderId);
        //    orderInfo.Channel = channel;

        //    var paymentApi = new PaymentApi(siteId);

        //    var amount = orderInfo.TotalFee;
        //    var orderNo = orderInfo.Guid;
        //    successUrl = $"{successUrl}?guid={orderNo}";
        //    if (channel == "alipay")
        //    {
        //        return isMobile
        //            ? paymentApi.ChargeByAlipayMobi(siteInfo.SiteName, amount, orderNo, successUrl)
        //            : paymentApi.ChargeByAlipayPc(siteInfo.SiteName, amount, orderNo, successUrl);
        //    }
        //    if (channel == "weixin")
        //    {
        //        var apiUrl = Context.PluginApi.GetPluginApiUrl(Main.PluginId);

        //        var notifyUrl = $"{apiUrl}/{nameof(StlShoppingPay.ApiPayWeixinNotify)}/{orderNo}";
        //        var url = HttpUtility.UrlEncode(paymentApi.ChargeByWeixin(siteInfo.SiteName, amount, orderNo, notifyUrl));
        //        var qrCodeUrl =
        //            $"{apiUrl}/{nameof(StlShoppingPay.ApiPayQrCode)}?qrcode={url}";
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

        //    return null;
        //}

        public static string Parse(IParseContext context)
        {
            var successUrl = string.Empty;
            var orderUrl = string.Empty;
            var weixinName = string.Empty;

            foreach (var attriName in context.StlAttributes.AllKeys)
            {
                var value = context.StlAttributes[attriName];
                if (Payment.Core.Utils.EqualsIgnoreCase(attriName, AttributeSuccessUrl))
                {
                    successUrl = Context.ParseApi.ParseAttributeValue(value, context);
                }
                else if (Payment.Core.Utils.EqualsIgnoreCase(attriName, AttributeOrderUrl))
                {
                    orderUrl = Context.ParseApi.ParseAttributeValue(value, context);
                }
                else if (Payment.Core.Utils.EqualsIgnoreCase(attriName, AttributeWeixinName))
                {
                    weixinName = Context.ParseApi.ParseAttributeValue(value, context);
                }
            }

            var paymentApi = new PaymentApi(context.SiteId);

            var html = Context.ParseApi.Parse(context.StlInnerHtml, context);
            if (string.IsNullOrEmpty(context.StlInnerHtml))
            {
                if (!string.IsNullOrEmpty(weixinName))
                {
                    weixinName = $@"<p style=""text-align: center"">{weixinName}</p>";
                }

                var htmlBuilder = new StringBuilder();
                htmlBuilder.Append(@"
<div class=""order-content"" v-show=""isUserLoggin"">
  <div class=""order-lists-a"">
    <a href=""javascript:;"" :class=""{ a_cut: type == '' }"" @click=""setOrderType('')"">全部订单</a>
    <a href=""javascript:;"" :class=""{ a_cut: type == 'True' }"" @click=""setOrderType('True')"">已支付</a>
    <a href=""javascript:;"" :class=""{ a_cut: type == 'False' }"" @click=""setOrderType('False')"">未支付</a>
  </div>

  <div v-for=""orderInfo in orderInfoList"">

    <div class="" order-sub "">
      <div class="" o-s-date "">
        {{ orderInfo.addDate }}
      </div>
      <div class="" o-s-id "">
        订单号：{{ orderInfo.guid }}
      </div>
      <div class="" o-s-total "">
        订单金额:
        <b>¥{{ getOrderFee(orderInfo) }}元</b>
      </div>
      <div class="" clear ""></div>
      <div class="" o-s-line ""></div>
      <div class="" o-s-person "">
        收货人：{{ orderInfo.realName }}
      </div>
      <div class="" o-s-address "">
        收货地址：{{ orderInfo.location + ' ' + orderInfo.address }}
      </div>
    </div>

    <div class="" order-table order-lists "">
      <div class="" o-t-title "">
        <div class="" o-t-name "">商品名称</div>
        <div class="" o-t-num "">数量</div>
        <div class="" o-t-price "">金额</div>
        <div class="" o-t-state "">状态</div>
        <div class="" o-t-operate "">操作</div>
      </div>
      <div class="" o-t-list "">
        <div class="" o-t-texts "">
          <ul>
            <li v-for=""cartInfo in orderInfo.cartInfoList"">
              <div class="" o-t-name "">
                <a :href=""cartInfo.linkUrl"">
                  <div class="" name-img "">
                    <img :src=""cartInfo.imageUrl"">
                  </div>
                  <p>{{ cartInfo.productName }}</p>
                </a>
              </div>
              <div class="" o-t-num "">
                <span>数量：</span>
                {{ cartInfo.count }}
              </div>
              <div class="" o-t-price "">
                <span>金额：</span>
                ¥{{ cartInfo.fee.toFixed(2) }}
              </div>
            </li>
          </ul>
        </div>
        <div class="" o-t-state "">
          <span>状态：</span>
            {{ getStateText(orderInfo.isPayed, orderInfo.state) }}
        </div>
        <div class="" o-t-operate "">
          <a href=""javascript:;"" @click=""openPay(orderInfo)"" class="" go-pay "" v-show=""!orderInfo.isPayed"">继续支付</a>
          <a href=""javascript:;"" class="" t-o-link "" @click=""viewOrder(orderInfo)"">查看</a>
          <a href=""javascript:;"" class="" t-o-link "" @click=""removeOrder(orderInfo)"">删除</a>
        </div>
      </div>
    </div>

  </div>

  <div class=""pages"" style=""display: none"">
    <a href=""#"" class=""page_f"">首页</a>
    <a href=""#"" class=""page_f"">上一页</a>
    <a href=""#"" class=""page_cut"">1</a>
    <a href=""#"">2</a>
    <a href=""#"">3</a>
    <a href=""#"">4</a>
    <a href=""#"">5</a>
    <a href=""#"" class=""page_f"">下一页</a>
    <a href=""#"" class=""page_f"">尾页</a>
  </div>
");
                htmlBuilder.Append($@"
<div class=""mask1_bg mask1_bg_cut"" v-show=""orderInfoToPay || isPaymentSuccess"" @click=""orderInfoToPay = isPaymentSuccess = false""></div>
<div class=""detail_alert detail_alert_cut"" v-show=""orderInfoToPay"">
  <div class=""close"" @click=""orderInfoToPay = isPaymentSuccess = false""></div>
  <div class=""alert_input"">
    金额: ¥{{{{ getOrderFee(orderInfoToPay) }}}}元
  </div>
  <div class=""pay_list"">
    <p>支付方式</p>
    <ul>
        <li v-show=""(isAlipayPc && !isMobile) || (isAlipayMobi && isMobile)"" :class=""{{ pay_cut: channel === 'alipay' }}"" @click=""channel = 'alipay'"" class=""channel_alipay""><b></b></li>
        <li v-show=""isWeixin"" :class=""{{ pay_cut: channel === 'weixin' }}"" @click=""channel = 'weixin'"" class=""channel_weixin""><b></b></li>
        <li v-show=""isJdpay"" :class=""{{ pay_cut: channel === 'jdpay' }}"" @click=""channel = 'jdpay'"" class=""channel_jdpay""><b></b></li>
    </ul>
    <div class=""mess_text""></div>
    <a href=""javascript:;"" @click=""pay"" class=""pay_go"">立即支付</a>
  </div>
</div>
<div class=""detail_alert detail_alert_cut"" v-show=""orderInfoToPay && isWxQrCode"">
  <div class=""close"" @click=""orderInfoToPay = isWxQrCode = isPaymentSuccess = false""></div>
  <div class=""pay_list"">
    <p style=""text-align: center""> 打开手机微信，扫一扫下面的二维码，即可完成支付</p>
    {weixinName}
    <p style=""margin-left: 195px;margin-bottom: 80px;""><img :src=""qrCodeUrl"" style=""width: 200px;height: 200px;""></p>
  </div>
</div>
<div class=""detail_alert detail_alert_cut"" v-show=""isPaymentSuccess"">
  <div class=""close"" @click=""orderInfoToPay = isWxQrCode = isPaymentSuccess = false""></div>
  <div class=""pay_list"">
    <p style=""text-align: center"">支付成功，谢谢支持</p>
    <div class=""mess_text""></div>
    <a href=""javascript:;"" @click=""weixinPayedClose"" class=""pay_go"">关闭</a>
  </div>
</div>
");
                htmlBuilder.Append("</div>");

                html = htmlBuilder.ToString();
            }

            var elementId = "el-" + Guid.NewGuid();
            var vueId = "v" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            html = $@"<div id=""{elementId}"" class=""shopping_order"">{html}</div>";

            var pluginUrl = Context.PluginApi.GetPluginUrl(Main.PluginId);
            var apiUrl = Context.Environment.ApiUrl;

            var jqueryUrl = $"{pluginUrl}/assets/js/jquery.min.js";
            var vueUrl = $"{pluginUrl}/assets/js/vue.min.js";
            var deviceUrl = $"{pluginUrl}/assets/js/device.min.js";
            var baseCssUrl = $"{pluginUrl}/assets/css/base.css";
            var orderCssUrl = $"{pluginUrl}/assets/css/order.css";
            var apiGetUrl = $"{apiUrl}/{Main.PluginId}/ApiOrdersGet";
            var apiRemoveOrderUrl = $"{apiUrl}/{Main.PluginId}/ApiOrdersRemove";
            var apiPayUrl = $"{apiUrl}/{Main.PluginId}/ApiOrdersPay";
            var apiWeixinIntervalUrl = $"{apiUrl}/{Main.PluginId}/ApiPayWeixinInterval";

            html += $@"
<script type=""text/javascript"" src=""{jqueryUrl}""></script>
<script type=""text/javascript"" src=""{vueUrl}""></script>
<script type=""text/javascript"" src=""{deviceUrl}""></script>
<link rel=""stylesheet"" type=""text/css"" href=""{baseCssUrl}"" />
<link rel=""stylesheet"" type=""text/css"" href=""{orderCssUrl}"" />
<script type=""text/javascript"">
    var match = location.search.match(new RegExp(""[\?\&]isPaymentSuccess=([^\&]+)"", ""i""));
    var isPaymentSuccess = (!match || match.length < 1) ? false : true;
    var {vueId} = new Vue({{
        el: '#{elementId}',
        data: {{
            type: '',
            isUserLoggin: false,
            orderInfoList: [],
            orderInfoToPay: null,
            isAlipay: {paymentApi.IsAliPay.ToString().ToLower()},
            isWeixin: {paymentApi.IsWxPay.ToString().ToLower()},
            isMobile: device.mobile(),
            channel: 'alipay',
            isWxQrCode: false,
            isPaymentSuccess: isPaymentSuccess,
            qrCodeUrl: ''
        }},
        methods: {{
            getStateText: function (isPayed, state) {{
                if (!isPayed) return '未支付';
                if (state == 'Done') return '已完成';
                return '已支付';
            }},
            getOrderFee: function(orderInfo) {{
                if (!orderInfo) return '';
                return (orderInfo.totalFee + orderInfo.expressCost).toFixed(2);
            }},
            setOrderType: function(type) {{
                {vueId}.type = type;
                {vueId}.orderInfoList = [];
                $.ajax({{
                    url : ""{apiGetUrl}"",
                    xhrFields: {{
                        withCredentials: true
                    }},
                    type: ""POST"",
                    data: JSON.stringify({{
                        type: type
                    }}),
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function(data)
                    {{
                        {vueId}.isUserLoggin = data.isUserLoggin;
                        {vueId}.orderInfoList = data.orderInfoList;
                    }},
                    error: function (err)
                    {{
                        var err = JSON.parse(err.responseText);
                        console.log(err.message);
                    }}
                }});
            }},
            viewOrder: function(orderInfo) {{
                location.href = '{orderUrl}?guid=' + orderInfo.guid;
            }},
            removeOrder: function (orderInfo) {{
                if (!confirm(""此操作将删除订单，确认吗？"")) return false;
                var index = this.orderInfoList.indexOf(orderInfo);
                if (index > -1) {{
                    this.orderInfoList.splice(index, 1);
                    $.ajax({{
                        url : ""{apiRemoveOrderUrl}"",
                        xhrFields: {{
                            withCredentials: true
                        }},
                        type: ""POST"",
                        data: JSON.stringify({{
                            orderId: orderInfo.id
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
            openPay: function (orderInfo) {{
                this.orderInfoToPay = orderInfo;
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
                                clearInterval(interval);
                                $this.orderInfoToPay = $this.isWxQrCode = false;
                                $this.isPaymentSuccess = true;
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
            weixinPayedClose: function() {{
                this.orderInfoToPay = this.isWxQrCode = this.isPaymentSuccess = false;
            }},
            pay: function () {{
                var $this = this;

                $.ajax({{
                    url : ""{apiPayUrl}"",
                    xhrFields: {{
                        withCredentials: true
                    }},
                    type: ""POST"",
                    data: JSON.stringify({{
                        siteId: {context.SiteId},
                        channel: this.channel,
                        orderId: this.orderInfoToPay.id,
                        isMobile: this.isMobile,
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
            }}
        }}
    }});
    $(document).ready(function(){{
        {vueId}.setOrderType('');
    }});
</script>
";

            return html;
        }
    }
}
